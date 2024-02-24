using UnityEditor.SceneManagement;
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
        // Load scene using the scene name or with path for testing.
        if (sceneName == null)
        {
            Debug.LogError("SceneLogic: No scene name or path provided.");
            return;
        }

        // Check if the scene name is a path.
        if (sceneName.Contains(".unity"))
        {
            EditorSceneManager.LoadSceneInPlayMode(sceneName, new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
