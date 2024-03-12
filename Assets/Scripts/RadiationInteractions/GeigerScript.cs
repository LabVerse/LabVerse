using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeigerScript : MonoBehaviour
{
    private TMP_Text geigerScreen;

    [SerializeField]
    private int m_geigerValue; // Min = 0, Max = 1000


    // Start is called before the first frame update
    void Start()
    {
        geigerScreen = GameObject.Find("GeigerCanvas/Text (TMP)").GetComponent<TMP_Text>();
        m_geigerValue = 18; //Default room CPM
    }

    // FixedUpdate is called every 0.2s
    void FixedUpdate()
    {
        // Update value on gameobject display
        UpdateDisplay();

        // Should geiger value decrement, or be set to zero, after each update?
        //m_geigerValue--;
    }
    public void ParticleHit(RadiationParticles.RADIATION_TYPES particle)
    {
        // Geiger Tube will call this on every collision
        // Adjust the value of the geiger counter as necessary
        m_geigerValue++;
        if (m_geigerValue > 1000) { m_geigerValue = 1000; } //cap at 1k cpm

    }

    private void UpdateDisplay()
    {
        string txt = m_geigerValue.ToString();
        geigerScreen.text = txt;
    }


}
