using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenOnClick : MonoBehaviour
{
    private BunsenBurner bunsenScript;
    // Start is called before the first frame update
    void Start()
    {
        bunsenScript = transform.parent.GetComponent<BunsenBurner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //toggle the bunsen flame between off, cool and hot
    public void ToggleFlame()
    {
        //swap between hot and cool flame
        bunsenScript.ToggleFlame();
    }
}
