using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Bacteria Growth Controller for spawning bacteria into petri dish.
/// </summary>
public class BacteriaGrowthController : MonoBehaviour
{
    [SerializeField] int bacteriumCount = 10;
    [SerializeField] GameObject bacteriumPrefab;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float growthRate = 0.01f;
    [SerializeField] float timePerGeneration = 1.0f;
    [SerializeField] float maxBacteriaSize = 1.0f;
    [SerializeField] Vector3 parentScale;
    List<GameObject> bacteria = new List<GameObject>();

    /// <summary>
    /// Spawn bacteria into petri dish.
    /// </summary>
    void Start()
    {
        ParentTransform parentTransform = GetComponentInParent<ParentTransform>();
        transform.localEulerAngles = parentTransform.rotation;
        parentScale = parentTransform.scale;
        spawnArea = new Vector3(spawnArea.x / parentScale.x, spawnArea.y / parentScale.y, spawnArea.z / parentScale.z);
        for (int i = 0; i < bacteriumCount; i++)
        {
            GameObject bacterium = Instantiate(bacteriumPrefab);
            bacterium.transform.SetParent(transform);
            bacterium.transform.localPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            Vector3 bacteriumScale = bacterium.transform.localScale;
            bacterium.transform.localScale = new Vector3(bacteriumScale.x / parentScale.x, bacteriumScale.y / parentScale.y, bacteriumScale.z / parentScale.z);
            BacteriaGrowth bacteriumGrowth = bacterium.GetComponent<BacteriaGrowth>();
            bacteriumGrowth.radius = GetComponent<SphereCollider>().radius * parentScale.x;
            bacteriumGrowth.growthRate = growthRate;
            bacteriumGrowth.timePerGeneration = timePerGeneration;
            bacteriumGrowth.maxBacteriaSize = maxBacteriaSize;
            bacteriumGrowth.parentScale = parentScale;
            bacteria.Add(bacterium);
        }
    }

    /// <summary>
    /// Draws spherical wire to show spawn area of bacteria.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnArea.x);
    }

    public List<GameObject> GetBacteria()
    {
        return bacteria;
    }
}
