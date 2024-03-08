using UnityEngine;
/// <summary>
/// Bacteria Solution Handler for applying bacteria to gameobject.
/// </summary>
public class BacterialSolutionHandler : MonoBehaviour
{
    /// <summary>
    /// Collider trigger event to place bacteria on gameobject with bacteria presence script.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        BacteriaPresence bacteriaPresence = other.gameObject.GetComponent<BacteriaPresence>();
        if (bacteriaPresence != null)
        {
            GameObject bacteria = bacteriaPresence.bacteria;
            bacteria.SetActive(true);
            bacteriaPresence.bacteriaPresent = true;
        }
    }
}
