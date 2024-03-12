using UnityEngine;

/// <summary>
/// Menu that enables the appropriate menu based on the completion of the experiment.
/// </summary>
public class CompletedMenuSelection : MonoBehaviour
{
    [SerializeField] 
    private GameObject m_completedMenu;

    [SerializeField]
    private GameObject m_notCompletedMenu;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("ExperimentCompletedStatus") == "completed")
        {
            m_completedMenu.SetActive(true);
        }
        else
        {
            m_notCompletedMenu.SetActive(true);
        }   
    }
}
