using UnityEngine;
using DG.Tweening;
/// <summary>
/// Sterilize loop handler for material change and stage progression.
/// </summary>
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
    /// <summary>
    /// Collider trigger event for checking stage changes, moving original material to red when in bunsen burner and toggling bacteria gameobject.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "BunsenFlame":
                {
                    // When the loop is sterilized, the bacteria is killed and the stage is progressed.
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
                    // Only stick to the loop if not in stage 0.
                    if (ExperimentManager.instance.currentStageIndex == 0)
                    {
                        StageManager.instance.FinishStage(0, false);
                    }   
                    
                    // When the loop touches the bacterial solution, the bacteria is active on the loop.
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
    /// <summary>
    /// Collider trigger event moving back to original material.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        objectLeftFlame = true;
        if (material.color != originalMaterial.color)
        {
            material.DOColor(originalMaterial.color, heatEffectLength);
        }
    }
}
