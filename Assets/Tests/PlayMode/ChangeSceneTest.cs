using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
public class ChangeSceneTest
{
    private Button m_button;

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/CurrentScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    /// <summary>
    /// Test to check if the button changes the scene.
    /// </summary>
    [UnityTest]
    public IEnumerator PressButtonChangeScene()
    {
        string startScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("CurrentScene", startScene);

        m_button = GameObject.Find("Return Button").GetComponent<Button>();
        Assert.IsNotNull(m_button);
        m_button.onClick.Invoke();

        // Wait one frame because the scene change is not immediate
        yield return null;

        string newScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("StartScene", newScene);

        yield return null;
    }
}