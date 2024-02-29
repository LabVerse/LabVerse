using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal_FlameTest : MonoBehaviour
{   
    private GameObject flame;
    private bool burning;

    //depending on element, set specific properties for flame colour, burn time, etc.
    public float countdown = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //check if collision was with a bunsen burner's flame
        if (other.gameObject.name == "BunsenFlame" && !burning)
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
