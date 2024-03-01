using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BacteriaGrowth : MonoBehaviour
{
    public float growthRate = 0.01f;
    public float timePerGeneration = 1.0f;
    public float maxBacteriaSize = 1.0f;
    public float radius = 0;
    Transform bacteriaTransform;
    double timeSinceLastGeneration = 0.0;
    Bounds meshBounds;
    float distanceFromCenter;

    // Start is called before the first frame update
    void Start()
    {
        bacteriaTransform = GetComponent<Transform>();
        timePerGeneration += Random.Range(-0.5f, 0.5f);
        maxBacteriaSize -= Random.Range(0.0f, maxBacteriaSize * 0.5f);
        meshBounds = GetComponent<MeshFilter>().mesh.bounds;
        distanceFromCenter = Mathf.Pow(transform.localPosition.x, 2) + Mathf.Pow(transform.localPosition.y, 2) + Mathf.Pow(transform.localPosition.z, 2); //Sqrt is expensive so don't use
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastGeneration >= timePerGeneration)
        {
            timeSinceLastGeneration = 0.0;
            bacteriaTransform.localScale += new Vector3(growthRate, 0, growthRate);
        }
        timeSinceLastGeneration += Time.deltaTime;

        Vector3 localBounds = new Vector3(meshBounds.max.x * transform.localScale.x, meshBounds.max.y * transform.localScale.y, meshBounds.max.z * transform.localScale.z);

        //If bacteria reached max size or container bounds, stop growing
        if (MaxSizeReached(localBounds) || ReachedContainerBounds(localBounds))
        {
            this.enabled = false;
        }
    }
    private bool MaxSizeReached(Vector3 localBounds)
    {
        return Mathf.Max(localBounds.x, localBounds.y, localBounds.z) >= maxBacteriaSize;
    }
    private bool ReachedContainerBounds(Vector3 localBounds)
    {
        float radiusOfBacteria = Mathf.Pow(localBounds.x, 2) + Mathf.Pow(localBounds.y,2) + Mathf.Pow(localBounds.z, 2);
        float radiusSquared = Mathf.Pow(radius, 2);
        return distanceFromCenter + radiusOfBacteria >= radiusSquared;
    }
}
