using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PetriDishEventController : MonoBehaviour
{
    [SerializeField] GameObject lid;
    [SerializeField] GameObject agarJelly;
    [SerializeField] GameObject bacteria;

    // Update is called once per frame
    void Update()
    {
        //if()
    }
    void OnTriggerEnter(Collider collider)
    {
        if (lid.activeSelf) return;

        switch(collider.gameObject.name)
        {
            case "Agar Flow":
                agarJelly.SetActive(true);
                break;
            case "Inoculating Loop":
                if (collider.gameObject.GetComponent<BacteriaPresence>())
                {
                    bacteria.SetActive(true);
                }
                break;
            case "Agar Plate":
                break;
            default:
                break;
        }
    }
}
