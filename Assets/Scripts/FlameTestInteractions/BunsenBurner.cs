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
        SetFlame(flameState);
    }

    // Update is called once per frame
    void Update()
    {
        //check if flame state has been changed
        // - how to do this? button on bunsen asset? discuss this later

        //set the flame to the correct state (would be better if this only occurs on change to state)
        SetFlame(flameState);
    }

    void SetFlame(int flameState)
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

    public bool IsLit()
    {
        return (flameState>0);
    }

    public void ToggleLit()
    {
        if (IsLit()) { SetFlame(0); }
        else { SetFlame(1); }
    }

    public void ToggleFlame()
    {
        if (flameState == 0) { flameState = 1; }
        else if (flameState == 1) { flameState = 2; }
        else if (flameState == 2) { flameState = 0; }
        //else do nothing
    }
}
