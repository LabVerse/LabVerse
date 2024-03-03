using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Metal_FlameTest : MonoBehaviour
{
    private GameObject flame;

    public int metal;
    private bool burning;

    //depending on element, set specific properties for flame colour, burn time, etc.
    public float countdown = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        //get references to key child components
        flame = transform.GetChild(0).gameObject;

        //set default values for attributes
        burning = false;
        flame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            //decrement countdown to zero
            countdown = countdown - (1 * Time.deltaTime);
            if(countdown <= 0)
            {
                //once zero hit, metal has 'burned up' so delete it
                Destroy(transform.gameObject);

                //call end of stage in stagehandler
                StageManager.instance.FinishStage(metal, true);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if collision was with a bunsen burner's flame
        if (other.transform.name == "BunsenFlameCool" || other.transform.name=="BunsenFlameHot")
        {
            //check if the bunsen burner's flame is lit
            if (other.gameObject.activeSelf)
            {
                burning = true;
                flame.SetActive(true);
            }
        }
    }
}
