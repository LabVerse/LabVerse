using UnityEngine;

/// <summary>
/// Controls the flames burning on metal attached to the inoculating loop
/// </summary>
public class MetalFlameBehaviour : MonoBehaviour
{
    [SerializeField] 
    private int stageIndex;

    private GameObject m_flame;
    private bool m_burning;

    // Depending on element, set specific properties for flame colour, burn time, etc. here:
    [SerializeField] private float countdown = 8f;
    // Other properties here:
    
    // Start is called before the first frame update
    void Start()
    {
        // Get references to key child components.
        m_flame = transform.GetChild(0).gameObject;

        // Set default values for attributes.
        m_burning = false;
        m_flame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_burning) BurnTick();
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
        if (other.transform.gameObject.name == "BunsenFlame")
        {
            // Check if the bunsen burner's flame is lit
            if (other.transform.parent.TryGetComponent<BunsenBurnerFlames>(out var bunsenBurner) && bunsenBurner.IsLit())
            {
                m_burning = true;
                m_flame.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if collision was with a bunsen burner's flame
        if (other.transform.gameObject.name == "BunsenFlame")
        {
            // Metal has left the flame, so stop burning
            m_burning = false;
            m_flame.SetActive(false);
        }
    }
}
