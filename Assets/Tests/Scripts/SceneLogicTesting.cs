using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls Scene changes.
/// </summary>
public class SceneLogicTesting : SceneLogic
{
    /// <summary>
    /// Changes Scene.
    /// </summary>
    public new void ChangeScene(string scenePath)
    {
        // Load scene using the scene path for testing.
        EditorSceneManager.LoadSceneInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Single));
    }
}
