using UnityEngine;

public class PetriDishEventController : MonoBehaviour
{
    public GameObject lid;
    [SerializeField] GameObject agarJelly;
    [SerializeField] GameObject bacteria;

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if (lid.activeSelf) return;

        switch(collider.gameObject.name)
        {
            case "Agar Flow":
                StageManager.instance.FinishStage(0, true);
                StageManager.instance.EnterStage(1);
                agarJelly.SetActive(true);
                //Shouldnt do this part
                StageManager.instance.FinishStage(1, true);
                StageManager.instance.EnterStage(2);
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
                    }
                    //3 to 4 in bacteria growth script
                }
                break;
            default:
                break;
        }
    }
}
