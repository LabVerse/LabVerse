using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenuTests
{
    private Button btn;

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/MainMenu.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator navigateToExperimentSelection()
    {
        string startScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("MainMenu", startScene);

        btn = GameObject.Find("Experiments button").GetComponent<Button>();
        Assert.IsNotNull(btn);
        btn.onClick.Invoke();

        yield return null;

        string newScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("SelectionMenu", newScene);

        yield return null;
    }

    [UnityTest]
    public IEnumerator navigateToSettings()
    {
        string startScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("MainMenu", startScene);

        btn = GameObject.Find("Settings button").GetComponent<Button>();
        Assert.IsNotNull(btn);
        btn.onClick.Invoke();

        yield return null;

        string newScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("SettingsMenu", newScene);

        yield return null;
    }
}