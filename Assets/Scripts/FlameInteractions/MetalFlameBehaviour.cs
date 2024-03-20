using UnityEngine;

/// <summary>
/// Controls the flames burning on metal attached to the inoculating loop
/// </summary>
public class MetalFlameBehaviour : MonoBehaviour
{
    [SerializeField] 
    private int stageIndex;

    private GameObject m_flame;

    // Depending on element, set specific properties for flame colour, burn time, etc. here:
    [SerializeField] private float m_countdown = 8f;
    // Other properties here:
    
    // Start is called before the first frame update
    void Start()
    {
        // Get references to key child components.
        m_flame = transform.GetChild(0).gameObject;

        // Set default values for attributes.
        m_flame.SetActive(false);
    }

    public void SetBurning(bool burning)
    {
        m_flame.SetActive(burning);
    }

    public void SetCountdown(float value = 8f)
    {
        m_countdown = value;
    }

    public float GetCountdown()
    {
        return m_countdown;
    }

    public int GetStageIndex()
    {
        return stageIndex;
    }

    public bool IsBurning()
    {
        return m_flame.activeSelf;
    }
}

