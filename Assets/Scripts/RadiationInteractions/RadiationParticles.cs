using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

/// <summary>
/// Use to set rlevant properties for different decay types
/// </summary>
public class RadiationParticles : MonoBehaviour
{
    public enum RADIATION_TYPES {ALPHA, BETA, GAMMA}

    [SerializeField]
    private RADIATION_TYPES particle;

    [SerializeField]
    private float quantity;

    private ParticleSystem particleSystem;
    private GeigerScript geigerScript;

    private float speed;
    private float lifetime;
    private int penetration;


    // Start is called before the first frame update
    void Start()
    {
         // Set starting values, depending on particle type
         // Different speed, size, lifetime, quantity, etc.
         switch(particle)
        {
            case RADIATION_TYPES.BETA:
                speed = 1f;
                lifetime = 1f;
                penetration = 1;
                break;

            case RADIATION_TYPES.GAMMA:
                speed = 1f;
                lifetime = 1f;
                penetration = 2;
                break;

            default: // case ALPHA
                speed = 1f;
                lifetime = 1f;
                penetration = 0;
                break;
        }

        particleSystem = gameObject.GetComponent<ParticleSystem>();
        // Set these values in the particle system component
        particleSystem.startSpeed = speed;
        // so on...
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if(other.layer!=6) return;
        geigerScript = other.transform.parent.GetComponent<GeigerScript>();
        geigerScript.ParticleHit(particle);
    }

}
