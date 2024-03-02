using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : MonoBehaviour
{
    private GameObject coolFlame;
    private GameObject hotFlame;
    public int flameState;

    // Start is called before the first frame update
    void Start()
    {
        coolFlame = transform.GetChild(0).gameObject;
        hotFlame = transform.GetChild(1).gameObject;

        flameState = 1;
        setFlame(flameState);
    }

    // Update is called once per frame
    void Update()
    {
        //check if flame state has been changed
        // - how to do this? button on bunsen asset? discuss this later

        //set the flame to the correct state (would be better if this only occurs on change to state)
        setFlame(flameState);
    }

    void setFlame(int flameState)
    {
        if (flameState == 0) { 
            //hide flame
            coolFlame.SetActive(false);
            hotFlame.SetActive(false);
        } 
        else if (flameState == 1)
        {
            //make cool flame
            coolFlame.SetActive(true);
            hotFlame.SetActive(false);


        }
        else if (flameState == 2)
        {
            //make hot flame
            hotFlame.SetActive(true);
            coolFlame.SetActive(false);

        }
    }

    public bool isLit()
    {
        return (flameState>0);
    }
}
