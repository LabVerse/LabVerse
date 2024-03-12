using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RadiationSource : MonoBehaviour
{
    /*
    // Define special data types
    public enum RADIATION_TYPES { ALPHA, BETA, GAMMA };

    [SerializeField]
    private Dictionary<RADIATION_TYPES, float> radiationDistances = new Dictionary<RADIATION_TYPES, float>
        {
            {RADIATION_TYPES.ALPHA, 1f},
            {RADIATION_TYPES.BETA, 1f},
            {RADIATION_TYPES.GAMMA, 1f}
        };

    // Important info about particle
    [SerializeField]
    private RADIATION_TYPES[] m_radiationTypes;



    // Start is called before the first frame update
    void Start()
    {
        // Set speed, radioactivity, etc.
        // Changes depending on the radioactive sample used
    }

    // FixedUpdate is called every 0.2s by default
    void FixedUpdate()
    {
        Decay();
    }

    /// <summary>
    /// Use to simulate the particle decaying, don't use update/
    /// </summary>
    private void Decay(int alpha=0, int beta=0, int gamma=0)
    {
        // Simulate one tick of decay
        
        ReleaseParticle(RADIATION_TYPES.ALPHA, alpha);

        ReleaseParticle(RADIATION_TYPES.BETA, beta);

        ReleaseParticle(RADIATION_TYPES.GAMMA, gamma); //not ideal, use for now
    }

    /// <summary>
    /// Releases alpha radiation
    /// </summary>
    /// <param name="particle">Define radiation type (alpha/beta)</param>
    /// <param name="quantity">Number of particles to release</param>
    private void ReleaseParticle(RADIATION_TYPES particle, int quantity)
    {
        // KEY QUESTION: DO WE WANT TO SEE PARTICLES? ALL? 1/100th? ...
        for (int i = 0; i < quantity; i++)
        {
            // Create instance of particle

            // Choose random direction, apply force

            // Move particle through space, until collision or timeout
        }
    }
    */
}
