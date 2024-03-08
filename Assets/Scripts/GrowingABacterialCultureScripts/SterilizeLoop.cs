using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SterilizeLoop : MonoBehaviour
{
    Material originalMaterial;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        originalMaterial = new Material(material);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "BunsenFlame":
                {
                StageManager.instance.FinishStage(1, true);
                StageManager.instance.EnterStage(2);
                BacteriaPresence bacteriaPresence = GetComponent<BacteriaPresence>();
                bacteriaPresence.bacteriaPresent = false;
                bacteriaPresence.bacteria.SetActive(false);
                if (material.color != Color.red)
                {
                    material.DOColor(Color.red, 10);
                }
                break;
                }
            case "bacterialSolution":
                {
                    BacteriaPresence bacteriaPresence = GetComponent<BacteriaPresence>();
                    if (bacteriaPresence != null && material.color == originalMaterial.color)
                    {
                        GameObject bacteria = bacteriaPresence.bacteria;
                        bacteria.SetActive(true);
                        bacteriaPresence.bacteriaPresent = true;
                    }
                }
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "BunsenFlame":
                if (material.color != originalMaterial.color)
                {
                    material.DOColor(originalMaterial.color, 4);
                }
                break;
        }
    }
}
