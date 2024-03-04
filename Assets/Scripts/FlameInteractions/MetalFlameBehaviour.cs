using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

/// <summary>
/// Controls the flames burning on metal attached to the inoculating loop
/// </summary>
public class MetalFlameBehaviour : MonoBehaviour
{
    [SerializeField] private int stageIndex;
    private GameObject m_flame;
    private bool m_burning;

    // Depending on element, set specific properties for flame colour, burn time, etc. here:
    [SerializeField] private float countdown = 8f;
    // Other properties here:
    
    // Start is called before the first frame update
    void Start()
    {
        //get references to key child components
        m_flame = transform.GetChild(0).gameObject;

        //set default values for attributes
        m_burning = false;
        m_flame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_burning) { BurnTick(); }    
    }

    /// <summary>
    /// Register one frame of the metal burning, if countdown hits zero destroy the gameobject
    /// </summary>
    private void BurnTick()
    {
        // Decrement countdown to zero
        countdown -= (1 * Time.deltaTime);
        if (countdown <= 0)
        {
            // Once zero hit, metal has 'burned up' so delete it
            Destroy(transform.gameObject);

            // Call end of stage in stage manager
            StageManager.instance.FinishStage(stageIndex, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if collision was with a bunsen burner's flame
        if (other.transform.gameObject.name == "BunsenBurner")
        {
            // Check if the bunsen burner's flame is lit
            BunsenBurnerFlames bunsenScript = other.transform.GetComponent<BunsenBurnerFlames>();
            if (bunsenScript.IsLit())
            {
                m_burning = true;
                m_flame.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if collision was with a bunsen burner's flame
        if (other.transform.gameObject.name == "BunsenBurner")
        {
            // Metal has left the flame, so stop burning
            m_burning = false;
            m_flame.SetActive(false);
        }
    }
}
