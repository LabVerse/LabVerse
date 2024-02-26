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
        // Load scene using the scene name.
        if (sceneName == null)
        {
            Debug.LogError("SceneLogic: No scene name provided.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
