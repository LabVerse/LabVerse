using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaGrowthController : MonoBehaviour
{
    [SerializeField] int bacteriumCount = 10;
    [SerializeField] GameObject bacteriumPrefab;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float growthRate = 0.01f;
    [SerializeField] float timePerGeneration = 1.0f;
    [SerializeField] float maxBacteriaSize = 1.0f;
    [SerializeField] Vector3 parentScale;
    // Start is called before the first frame update
    void Start()
    {
        parentScale = GetComponentInParent<ContainerScale>().scale;
        spawnArea = new Vector3(spawnArea.x / parentScale.x, spawnArea.y / parentScale.y, spawnArea.z / parentScale.z);
        for (int i = 0; i < bacteriumCount; i++)
        {
            GameObject bacteria = Instantiate(bacteriumPrefab);
            bacteria.transform.SetParent(transform);
            bacteria.transform.localPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            BacteriaGrowth bacteriaGrowth = bacteria.GetComponent<BacteriaGrowth>();
            bacteriaGrowth.radius = GetComponent<SphereCollider>().radius * parentScale.x;
            bacteriaGrowth.growthRate = growthRate;
            bacteriaGrowth.timePerGeneration = timePerGeneration;
            bacteriaGrowth.maxBacteriaSize = maxBacteriaSize;
            bacteriaGrowth.parentScale = parentScale;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnArea.x);
    }
}
