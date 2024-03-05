using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionMenuTests
{
    private Button btn;

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/SelectionMenu.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator NavigateToMainMenu()
    {
        string startScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("SelectionMenu", startScene);

        btn = GameObject.Find("Back button").GetComponent<Button>();
        Assert.IsNotNull(btn);
        btn.onClick.Invoke();

        yield return null;

        string newScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("MainMenu", newScene);

        yield return null;
    }

    [UnityTest]
    public IEnumerator FilterSearchOptions()
    {
        GameObject searchBarObject = GameObject.Find("Search bar");
        Assert.IsNotNull(searchBarObject, "Search bar GameObject not found in the scene");
        TMP_InputField searchBarInputField = searchBarObject.GetComponent<TMP_InputField>();
        Assert.IsNotNull(searchBarInputField, "InputField component not found on the SearchBar GameObject");

        string searchText = "flame";
        searchBarInputField.text = searchText;
        searchBarInputField.onEndEdit.Invoke(searchText);

        yield return null;

        GameObject flameTestOption = GameObject.Find("Flame Test option");
        Assert.IsNotNull(flameTestOption, "Flame Test option GameObject not found in the scene");
        Assert.IsTrue(flameTestOption.activeSelf, "Flame Test option GameObject is not active");
        GameObject microorganismsOption = GameObject.Find("Microorganisms option");
        Assert.IsNull(microorganismsOption, "Microorganisms option GameObject found in the scene, but does not match search query");
        
        yield return null;
    }
}
