using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PourHandler : MonoBehaviour
{
    [SerializeField] GameObject cap;
    [SerializeField] GameObject agarFlow;
    [SerializeField] GameObject petriDish;
    
    // Update is called once per frame
    void Update()
    {
        if (cap.activeSelf) return;

        if(Mathf.Abs(transform.rotation.x) > 0.8)
        {
            agarFlow.SetActive(true);
        }
        else
        {             
            agarFlow.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InputPhoneManager>() != null || other.gameObject.GetComponentInParent<InputActionManager>() != null)
        {
            cap.SetActive(!cap.activeSelf);
        }
    }
}
