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
        //set the flame to the correct state (would be better if this only occurs on change to state)
        SetFlame(flameState);
    }

    public bool SetFlame(int flame)
    {
        if (flame == 0) {
            //hide flame
            coolFlame.SetActive(false);
            hotFlame.SetActive(false);
        } 
        else if (flame == 1)
        {
            //make cool flame
            coolFlame.SetActive(true);
            hotFlame.SetActive(false);
        }
        else if (flame == 2)
        {
            //make hot flame
            coolFlame.SetActive(false);
            hotFlame.SetActive(true);

        }
        else
        {
            return false;
        }
        flameState = flame;
        return true;
    }

    public bool IsLit()
    {
        return (flameState>0);
    }

    public void ToggleFlame()
    {
        if (flameState == 0) { SetFlame(1); }
        else if (flameState == 1) { SetFlame(2); }
        else if (flameState == 2) { SetFlame(0); }
        //else do nothing
    }
}
