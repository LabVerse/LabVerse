using System;
using TMPro;
using UnityEngine;

public class GeigerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text geigerScreen;

    [SerializeField]
    private int m_geigerValue = 0; // Min = 0, Max = 1000
    private float m_countdown = 0;
    [SerializeField] private float m_delay = 0.7f;
    [SerializeField] private int m_backgroundRadiation = 18;
    int m_radiationSum = 0;
    int m_updateCount = 0;

    [SerializeField]
    private GameObject[] barriers;

    private int m_currentBarrierIndex = -1;

    // Update is called every frame
    void Update()
    {
        m_radiationSum += m_geigerValue;
        m_updateCount++;
        m_geigerValue = 0;
        m_countdown -= Time.deltaTime;

        if(m_countdown <= 0)
        {
            // Calculate new CPM
            UpdateCPM();

            //Reset countdown
            m_countdown = m_delay;
        }
    }

    /// <summary>
    /// Call this whenever a particle colliders with the geiger sensor's collider.
    /// </summary>
    /// <param name="particle">Radiation (alpha/beta/gamma) of type RADIATION_TYPES</param>
    public void ParticleHit(RadiationParticles.RADIATION_TYPES particle)
    {
        // Geiger Tube will call this on every collision
        // Adjust the value of the geiger counter as necessary
        switch (particle) {
            case RadiationParticles.RADIATION_TYPES.GAMMA:
                m_geigerValue += GaussianRandom(100,10);//100 to 10,000 CPM
                break;
            case RadiationParticles.RADIATION_TYPES.BETA:
                m_geigerValue += GaussianRandom(10,1);//10 to 1000 CPM
                break;
            case RadiationParticles.RADIATION_TYPES.ALPHA:
                m_geigerValue += GaussianRandom(1,0.1f);//1 to 1000 CPM
                break;
            default:
                break;
        }

    }

    private void UpdateCPM()
    {
        //Calclulate the average
        int avg = (int)(m_radiationSum * 60 / m_updateCount / m_delay) + m_backgroundRadiation;

        // Display average to screen
        string txt = avg.ToString();
        geigerScreen.text = txt;
        m_radiationSum = 0;
        m_updateCount = 0;
    }

    private int GaussianRandom(float mean, float stdDev)
    {
        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

        return Mathf.Abs((int)randNormal);
    }

    public void ToggleBarrier()
    {
        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(false);
        }

        m_currentBarrierIndex++;
        if (m_currentBarrierIndex >= barriers.Length)
        {
            m_currentBarrierIndex = -1;
        }

        if (m_currentBarrierIndex == -1) return;

        barriers[m_currentBarrierIndex].SetActive(true);
    }
}
