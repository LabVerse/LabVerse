using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacterialSolutionHandler : MonoBehaviour
{
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
