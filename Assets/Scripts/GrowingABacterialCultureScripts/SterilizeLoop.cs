using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SterilizeLoop : MonoBehaviour
{
    Material originalMaterial;
    Material material;
    float counter;
    int heatEffectLength = 4;
    bool objectLeftFlame = false;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        originalMaterial = new Material(material);
    }
    void Update()
    {
        if (objectLeftFlame && material.color != originalMaterial.color)
        {
            counter += Time.deltaTime;
            if (counter >= heatEffectLength)
            {
                material.SetColor("_Color",originalMaterial.color);
                counter = 0;
            }
        }
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
                material.DOColor(Color.red, heatEffectLength);
                objectLeftFlame = false;
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
        objectLeftFlame = true;
        if (material.color != originalMaterial.color)
        {
            material.DOColor(originalMaterial.color, heatEffectLength);
        }
    }
}
