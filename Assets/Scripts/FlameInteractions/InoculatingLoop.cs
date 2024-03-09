using UnityEngine;

/// <summary>
/// Controls the inoculating loop prefab 'picking up' a sample of whatever metal it collides with
/// </summary>
public class InoculatingLoop : MonoBehaviour
{
    private GameObject m_copperSample;
    private GameObject m_lithiumSample;
    private GameObject m_ironSample;

    private GameObject m_activeMetal;

    private float m_countdown = 8f;

    private void Start()
    {
        //get reference to copper, lithium and iron samples here
        m_copperSample = transform.GetChild(0).gameObject;
        m_lithiumSample = transform.GetChild(1).gameObject;
        m_ironSample = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (m_activeMetal == null) return;
        if(m_activeMetal.GetComponent<MetalFlameBehaviour>().IsBurning())
        {
            BurnTick();
        }
    }

    /// <summary>
    /// When the inoculating loop collides with a metal source, remove any other child metals and add the current one
    /// <\summary>
    private void OnTriggerEnter(Collider other)
    {
        // Check if collision was with a bunsen burner's flame
        if (other.transform.gameObject.name == "BunsenFlame")
        {
            // If no metal sample on the inoculating loop, return
            if (m_activeMetal == null) return;

            // Check if the bunsen burner's flame is lit
            if (other.transform.parent.TryGetComponent<BunsenBurnerFlames>(out var bunsenBurner) && bunsenBurner.IsLit())
            {
                //get active metal sample, set m_isBurning
                m_activeMetal.GetComponent<MetalFlameBehaviour>().SetBurning(true);
 
            }
        }

        // Otherwise, check if collision is with metal source
        if (!other.gameObject.CompareTag("Metal")) return;


        // Fail the previous metal's stage
        if (m_activeMetal != null)
        {
            StageManager.instance.FinishStage(m_activeMetal.GetComponent<MetalFlameBehaviour>().GetStageIndex(), false);
        }

        // Determine which metal should be added to the loop
        int metalIndex;
        switch (other.gameObject.name)
        {
            case "CopperSource":
                //set copper sample active
                SetSample(m_copperSample);
                metalIndex = 0;
                break;
            case "LithiumSource":
                //set lithium sample active
                SetSample(m_lithiumSample);
                metalIndex = 1;
                break;
            case "IronSource":
                //set iron sample active
                SetSample(m_ironSample);
                metalIndex = 2;
                break;
            default:
                return;
        }

        // Start the correct stage of the experiment.
        // Add one because first stage is turning the bunsen burner on.
        StageManager.instance.EnterStage(metalIndex + 1);
    }

    /// <summary>
    /// Helper function to change the metal on the end of the inoculating loop
    /// </summary>
    private void SetSample(GameObject metal)
    {
        float countdown = metal.GetComponent<MetalFlameBehaviour>().GetCountdown();
        //reset countdown on all inactive metals
        m_copperSample.GetComponent<MetalFlameBehaviour>().SetCountdown();
        m_lithiumSample.GetComponent<MetalFlameBehaviour>().SetCountdown();
        m_ironSample.GetComponent<MetalFlameBehaviour>().SetCountdown();

        //set all metals to inactive
        m_copperSample.SetActive(false);
        m_lithiumSample.SetActive(false);
        m_ironSample.SetActive(false);

        //set correct metal to active
        metal.GetComponent<MetalFlameBehaviour>().SetCountdown(countdown);
        m_countdown = metal.GetComponent<MetalFlameBehaviour>().GetCountdown();
        metal.SetActive(true);
        m_activeMetal = metal;
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if collision was with a bunsen burner's flame
        if (other.transform.gameObject.name == "BunsenFlame")
        {
            //get child metal sample, set inactive and m_isBurning to false
            if (m_activeMetal == null) return;
            m_activeMetal.GetComponent<MetalFlameBehaviour>().SetBurning(false);
        }
    }

    /// <summary>
    /// Register one frame of the metal burning, if countdown hits zero destroy the gameobject
    /// </summary>
    private void BurnTick()
    {
        // Decrement countdown to zero
        m_countdown -= (1 * Time.deltaTime);
        if (m_countdown <= 0)
        {
            // Once zero hit, metal has 'burned up' so delete it
            int stageIndex = m_activeMetal.GetComponent<MetalFlameBehaviour>().GetStageIndex();
            m_activeMetal.GetComponent<MetalFlameBehaviour>().SetBurning(false);
            m_activeMetal.SetActive(false);
            m_activeMetal = null;

            // Call end of stage in stage manager
            StageManager.instance.FinishStage(stageIndex, true);
        }
    }
}
