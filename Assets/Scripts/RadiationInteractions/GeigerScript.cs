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

    private int prev;


    // Start is called before the first frame update
    void Start()
    {
        geigerScreen = GameObject.Find("GeigerCanvas/Text (TMP)").GetComponent<TMP_Text>();
        m_geigerValue = 0;
        prev = 18; // default CPM, standard room radioactivity
    }

    // FixedUpdate is called every 0.2s
    void FixedUpdate()
    {
        // Update value on gameobject display, whenever geiger value changes
        if (m_geigerValue != prev)
        {
            prev = m_geigerValue;
            UpdateDisplay();
        }

        // Should geiger value decrement, or be set to zero, after each update?
        
    }
    public void ParticleHit(RadiationParticles.RADIATION_TYPES particle)
    {
        // Radioactive sample will call this on every collision

        // Adjust the value of the geiger counter as necessary


    }

    private void UpdateDisplay()
    {
        string txt = m_geigerValue.ToString();
        geigerScreen.text = txt;
    }


}
