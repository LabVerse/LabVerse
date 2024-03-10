using UnityEngine;
/// <summary>
/// Petri Dish Event Controller for handling stages that directly require petri dish gameobject.
/// </summary>
public class PetriDishEventController : MonoBehaviour
{
    public GameObject lid;
    public GameObject agarJelly;
    public GameObject bacteria;
    /// <summary>
    /// Collider trigger event for checking stage changes and enabling agar jelly and bacteria gameobject component.
    /// </summary>
    void OnTriggerEnter(Collider collider)
    {
        if (lid.activeSelf) return;

        switch(collider.gameObject.name)
        {
            case "Agar Flow":
                StageManager.instance.FinishStage(0, true);
                StageManager.instance.EnterStage(1);
                agarJelly.SetActive(true);
                
                break;
            case "loop":
                BacteriaPresence bacteriaPresence = collider.gameObject.GetComponent<BacteriaPresence>();
                if (bacteriaPresence != null && agarJelly.activeSelf)
                {
                    if(bacteriaPresence.bacteriaPresent)
                    {
                        StageManager.instance.FinishStage(2, true);
                        StageManager.instance.EnterStage(3);
                        bacteria.SetActive(true);
                        collider.gameObject.GetComponent<BacteriaPresence>().bacteria.SetActive(false);
                        collider.gameObject.GetComponent<BacteriaPresence>().bacteriaPresent = false;
                    }
                    //3 to 4 in bacteria growth script
                }
                break;
            default:
                break;
        }
    }
}
