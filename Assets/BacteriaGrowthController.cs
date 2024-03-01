using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaGrowthController : MonoBehaviour
{
    [SerializeField] int bacteriaCount = 10;
    [SerializeField] GameObject bacteriaPrefab;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float growthRate = 0.01f;
    [SerializeField] float timePerGeneration = 1.0f;
    [SerializeField] float maxBacteriaSize = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bacteriaCount; i++)
        {
            GameObject bacteria = Instantiate(bacteriaPrefab);
            bacteria.transform.SetParent(transform);
            bacteria.transform.localPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            BacteriaGrowth bacteriaGrowth = bacteria.GetComponent<BacteriaGrowth>();
            bacteriaGrowth.radius = this.GetComponent<SphereCollider>().radius;
            bacteriaGrowth.growthRate = growthRate;
            bacteriaGrowth.timePerGeneration = timePerGeneration;
            bacteriaGrowth.maxBacteriaSize = maxBacteriaSize;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnArea.x);
    }
}
