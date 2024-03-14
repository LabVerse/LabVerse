using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFlameAudio : MonoBehaviour
{
    AudioSource source;
    BunsenBurnerFlames bunsenScript;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        bunsenScript = GameObject.Find("BunsenBurner").GetComponent<BunsenBurnerFlames>();
    }

    void Update()
    {
        if (bunsenScript.IsLit() && !source.isPlaying)
        {
            source.Play();
        }
        else if (!bunsenScript.IsLit() && source.isPlaying)
        {
            source.Stop();
        }
    }
}
