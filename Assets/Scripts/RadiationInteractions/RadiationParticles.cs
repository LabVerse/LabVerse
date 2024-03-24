using UnityEngine;

/// <summary>
/// Use to set relevant properties for different decay types.
/// </summary>
public class RadiationParticles : MonoBehaviour
{
    public enum RADIATION_TYPES {ALPHA, BETA, GAMMA}

    [SerializeField]
    private RADIATION_TYPES particle;

    [SerializeField]
    private float quantity;

    private ParticleSystem m_particleSystem;
    private GeigerScript geigerScript;

    // private int penetration;


    // Start is called before the first frame update
    void Start()
    {
        m_particleSystem = gameObject.GetComponent<ParticleSystem>();
        
        // Set release/second here
        // MUST first declare emission as var, not sure why one-liner doesn't work D:
        var em = m_particleSystem.emission;
        em.rateOverTime = quantity;
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.layer!=6) return;

        geigerScript = other.transform.parent.GetComponent<GeigerScript>();
        geigerScript.ParticleHit(particle);
    }

}
