using UnityEngine;


/// <summary>
/// Controls all changes & checks regarding flames on the bunsen burner
/// </summary>
public class BunsenBurnerFlames : MonoBehaviour
{
    public enum FLAME_STATE{OFF, COOL, HOT};

    [SerializeField] 
    private FLAME_STATE m_flameState;

    private GameObject m_coolFlame;
    private GameObject m_hotFlame;

    // Start is called before the first frame update
    void Start()
    {
        Transform flameParent = transform.Find("BunsenFlame");
        m_coolFlame = flameParent.GetChild(0).gameObject;
        m_hotFlame = flameParent.GetChild(1).gameObject;

        m_flameState = FLAME_STATE.COOL;
        SetFlame(m_flameState);
    }

    /// <summary>
    /// Change the state of the bunsen burner
    /// </summary>
    /// <param name="flame">choose between three FLAME_STATE values: OFF, COOL, or HOT</param>
    /// <returns>True if the flame was set successfully, otherwise false</returns>
    public bool SetFlame(FLAME_STATE flame)
    {
        switch (flame)
        {
            case FLAME_STATE.OFF:
                // Hide flame
                m_coolFlame.SetActive(false);
                m_hotFlame.SetActive(false);
                break;
            case FLAME_STATE.COOL:
                // Make cool flame
                m_coolFlame.SetActive(true);
                m_hotFlame.SetActive(false);
                break;
            case FLAME_STATE.HOT:
                // Make hot flame
                m_coolFlame.SetActive(false);
                m_hotFlame.SetActive(true);
                break;
            default:
                return false;
        }

        m_flameState = flame;
        return true;
    }

    /// <summary>
    /// Check if a flame is currently burning on the bunsen burner
    /// </summary>
    public bool IsLit()
    {
        return m_flameState != FLAME_STATE.OFF;
    }

    /// <summary>
    /// For testing purposes only, can remove/comment this function out later
    /// </summary>
    /// <returns>The current flame on the bunsen burner (off, cool or hot)</returns>
    public FLAME_STATE GetFlameState()
    {
        return m_flameState;
    }

    /// <summary>
    /// Toggle between flame states, off >> cool >> hot >> off
    /// </summary>
    public void ToggleFlame()
    {
        // Toggle to the next state, ie off >> cool >> hot >> off
        switch (m_flameState)
        {
            case FLAME_STATE.OFF:
                SetFlame(FLAME_STATE.COOL);
                break;
            case FLAME_STATE.COOL: 
                SetFlame(FLAME_STATE.HOT); 
                break;
            case FLAME_STATE.HOT: 
                SetFlame(FLAME_STATE.OFF); 
                break;
        }
    }
}
