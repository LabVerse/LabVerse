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
}
