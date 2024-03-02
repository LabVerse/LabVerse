using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BacteriaGrowth : MonoBehaviour
{
    public float radiusOfBacteria;
    public float growthRate = 0.005f;
    public float timePerGeneration = 1.0f;
    public float maxBacteriaSize = 1.0f;
    public float radius = 0;
    double timeSinceLastGeneration = 0.0;
    Bounds meshBounds;
    [SerializeField] float distanceFromCenter;
    public Vector3 parentScale;
    [SerializeField] Vector3 localBounds;

    // Start is called before the first frame update
    void Start()
    {
        timePerGeneration += Random.Range(-0.5f, 0.5f);
        maxBacteriaSize -= Random.Range(0.0f, maxBacteriaSize * 0.5f);
        meshBounds = GetComponent<MeshFilter>().mesh.bounds;
        distanceFromCenter = Mathf.Pow(transform.localPosition.x * parentScale.x, 2) + Mathf.Pow(transform.localPosition.y * parentScale.y, 2) + Mathf.Pow(transform.localPosition.z * parentScale.z, 2); //Sqrt is expensive so don't use
        parentScale = GetComponentInParent<ContainerScale>().scale;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastGeneration >= timePerGeneration)
        {
            timeSinceLastGeneration = 0.0;
            transform.localScale += new Vector3(growthRate / parentScale.x, 0, growthRate / parentScale.z);
        }
        timeSinceLastGeneration += Time.deltaTime;

        localBounds = new Vector3(meshBounds.max.x * parentScale.x * transform.localScale.x, meshBounds.max.y * parentScale.y * transform.localScale.y, meshBounds.max.z * parentScale.z * transform.localScale.z);
        //If bacteria reached max size or container bounds, stop growing

        if (MaxSizeReached(localBounds) || ReachedContainerBounds(localBounds))
        {
            this.enabled = false;
        }
    }
    private bool MaxSizeReached(Vector3 localBounds)
    {
        return Mathf.Max(localBounds.x, localBounds.y, localBounds.z ) >= maxBacteriaSize;
    }
    private bool ReachedContainerBounds(Vector3 localBounds)
    {
        radiusOfBacteria = Mathf.Pow(localBounds.x, 2)+ Mathf.Pow(localBounds.y,2) + Mathf.Pow(localBounds.z, 2);
        float radiusSquared = Mathf.Pow(radius, 2);
        Debug.Log((distanceFromCenter + radiusOfBacteria)+" VS "+ radiusSquared);
        return distanceFromCenter + radiusOfBacteria >= radiusSquared;
    }
}
