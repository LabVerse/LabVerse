using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void goToSelectionMenu()
    {
        SceneManager.LoadScene("SelectionMenu");
    }
    public void goToSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void goToExperimentLab()
    {
        SceneManager.LoadScene("ExperimentLab");
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
