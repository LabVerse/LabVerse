using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void goToSelectionMenu()
    {
        SceneManager.LoadScene("ExperimentSelectionMenu");
    }
    public void goToSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void openDocumentation()
    {
        Application.OpenURL("https://github.com/LabVerse/LabVerse");
    }
    public void quitApp()
    {
        Application.Quit();
    }
}
