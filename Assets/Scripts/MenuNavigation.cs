using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void goToSelectionMenu()
    {
        SceneManager.LoadScene("ExperimentSelection");
    }
    public void goToSettings()
    {
        SceneManager.LoadScene("Settings");
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
