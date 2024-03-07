using UnityEngine;

/// <summary>
/// Controls the inoculating loop prefab 'picking up' a sample of whatever metal it collides with
/// </summary>
public class InoculatingLoop : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] metalSamples;

    /// <summary>
    /// When the inoculating loop collides with a metal source, remove any other child metals and add the current one
    /// <\summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Metal")) return;

        // Remove existing metal sample
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int metalIndex;
        // Determine which metal should be added to the loop
        switch (other.gameObject.name)
        {
            case "CopperSource":
                metalIndex = 0;
                break;
            case "LithiumSource":
                metalIndex = 1;
                break;
            case "IronSource":
                metalIndex = 2;
                break;
            default:
                return;
        }
        // Spawn the metal on the loop
        GameObject newChild = Instantiate(metalSamples[metalIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newChild.transform.parent = transform;

        Vector3 colliderPos = new Vector3(0.00465f, 0, 0);
        newChild.transform.localPosition = colliderPos;

        // Start the correct stage of the experiment
        StageManager.instance.EnterStage(metalIndex);
    }
}
