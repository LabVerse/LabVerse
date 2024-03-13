using Codice.CM.Client.Differences;
using System;
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

    private float m_countdown;
    private float m_delay;
    private int m_count;

    private LinkedList<int> m_values;

    // Start is called before the first frame update
    void Start()
    {
        // HOW TO DECREASE FLUCTUATIONS???
        // - increase particle count
        // - use average of previous n values
        // - update after longer wait

        geigerScreen = GameObject.Find("GeigerCanvas/Text (TMP)").GetComponent<TMP_Text>();
        
        m_delay = 1f;
        m_countdown = m_delay;

        m_geigerValue = (int) System.Math.Round(18/m_delay); //Default room CPM
        int cpm = (int)System.Math.Round(m_geigerValue * (60 / m_delay));

        m_values = new LinkedList<int>(); 
        for (int i=0; i<10; i++)
        {
            m_values.AddLast(cpm);
        }
    }

    // Update is called every frame
    void Update()
    {
        m_countdown -= 1*Time.deltaTime;

        if(m_countdown < 0)
        {
            // Calculate new CPM
            // Update Display
            if (m_count > 2)
            {
                m_count = 0;
                UpdateCPM(true);
            } else
            {
                m_count++;
                UpdateCPM(false);
            }

            // Should geiger value decrement, or be set to zero, after each update?
            m_geigerValue = 0;

            //Reset countdown
            m_countdown = m_delay;
        }
    }
    public void ParticleHit(RadiationParticles.RADIATION_TYPES particle)
    {
        // Geiger Tube will call this on every collision
        // Adjust the value of the geiger counter as necessary
        switch (particle) {
            case RadiationParticles.RADIATION_TYPES.GAMMA:
                m_geigerValue += 1;
                break;
            case RadiationParticles.RADIATION_TYPES.BETA:
                m_geigerValue += 3;
                break;
            default: //RadiationParticles.RADIATION_TYPES.ALPHA
                m_geigerValue += 5;
                break;
        }

    }

    private void UpdateCPM(bool update)
    {
        // Convert from collisions/update to CPM
        // Currently updates every 2s, so x30
        int CPM = (int) System.Math.Round(m_geigerValue * (60/m_delay));
        if (CPM > 1000) { CPM = 1000; } //cap at 1k cpm

        // Remove oldest reading
        m_values.RemoveFirst();
        m_values.AddLast(CPM);

        if (update)
        {
            // Calculate average
            float total = 0;
            float i = 0;
            LinkedListNode<int> head = m_values.First;
            while (head != null)
            {
                total += head.Value;
                head = head.Next;
                i++;
            }

            // Display average to screen
            int avg = (int)System.Math.Round(total / i);
            string txt = avg.ToString();
            geigerScreen.text = txt;
        }
    }
}
