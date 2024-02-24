using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls Scene changes.
/// </summary>
public class SceneLogic : MonoBehaviour
{
    /// <summary>
    /// Changes Scene.
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
}
