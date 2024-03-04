using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class PourHandler : MonoBehaviour
{
    [SerializeField] GameObject cap;
    [SerializeField] GameObject agarFlow;
    [SerializeField] ParentTransform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponentInParent<ParentTransform>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (cap.activeSelf) return;
        float rotationX = Mathf.Abs(parentTransform.rotation.x);
        float rotationZ = Mathf.Abs(parentTransform.rotation.z);
        if (rotationX > 180) rotationX = 360 - rotationX;
        if (rotationZ > 180) rotationZ = 360 - rotationZ;

        if(rotationX > 90 || rotationZ > 90)
        {
            agarFlow.SetActive(true);
        }
        else
        {             
            agarFlow.SetActive(false);
        }
    }

    public void OnCapClicked()
    {
        cap.SetActive(!cap.activeSelf);
    }
}
