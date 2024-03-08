using UnityEngine;
/// <summary>
/// Bacteria Growth for deciding bacterium conditions and growing bacterium while in petri dish.
/// </summary>
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
    public GameObject lid;
    bool experimentStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //Randomize time per generation and max bacteria size
        timePerGeneration += Random.Range(-0.5f, 0.5f);
        maxBacteriaSize -= Random.Range(0.0f, maxBacteriaSize * 0.5f);

        //Used in checking bacterium bounds against container bounds
        meshBounds = GetComponent<MeshFilter>().mesh.bounds;
        distanceFromCenter = Mathf.Pow(transform.localPosition.x * parentScale.x, 2) + Mathf.Pow(transform.localPosition.y * parentScale.y, 2) + Mathf.Pow(transform.localPosition.z * parentScale.z, 2); //Sqrt is expensive so don't use
        parentScale = GetComponentInParent<ParentTransform>().scale;

        lid = GetComponentInParent<PetriDishEventController>().lid;

        //Deviate colour between bacterium
        GetComponent<MeshRenderer>().material.color *= Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (lid == null || !lid.activeSelf) return; //Only grow if the lid is on

        if (experimentStarted)
        {
            Grow();
        }
        else
        {
            //Move to next stage
            experimentStarted = true;
            StageManager.instance.FinishStage(3, true);
            StageManager.instance.EnterStage(4);
        }
    }

    /// <summary>
    /// Scales bacterium at specific interval. Is not affected by parent scale.
    /// </summary>
    private void Grow() { 
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

    /// <summary>
    /// Checks if bacterium has reached max size.
    /// </summary>
    private bool MaxSizeReached(Vector3 localBounds)
    {
        return Mathf.Max(localBounds.x, localBounds.y, localBounds.z ) >= maxBacteriaSize;
    }

    /// <summary>
    /// Checks if bacterium has reached container bounds.
    /// </summary>
    private bool ReachedContainerBounds(Vector3 localBounds)
    {
        radiusOfBacteria = Mathf.Pow(localBounds.x, 2)+ Mathf.Pow(localBounds.y,2) + Mathf.Pow(localBounds.z, 2);
        float radiusSquared = Mathf.Pow(radius, 2);
        return distanceFromCenter + radiusOfBacteria >= radiusSquared;
    }
}
