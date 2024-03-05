using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class BunsenBurnerTestScript
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Testing/Kai/UnitTestScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator SetFlameTests()
    {
        GameObject bunsen = GameObject.Find("BunsenBurner");
        var bunsenScript = bunsen.GetComponent<BunsenBurner>();
        GameObject coolFlame = bunsen.transform.GetChild(0).gameObject;
        GameObject hotFlame = bunsen.transform.GetChild(1).gameObject;

        //check that the bunsen burner can switch between states as intended
        //1. turn flame off
        bool success = bunsenScript.SetFlame(0);
        bool flamesOn = coolFlame.activeSelf || hotFlame.activeSelf || bunsenScript.IsLit();
        Assert.IsTrue(success && !flamesOn);

        //2. set to cool flame
        success = bunsenScript.SetFlame(1);
        flamesOn = coolFlame.activeSelf && bunsenScript.flameState==1 && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //3. set to hot flame
        success = bunsenScript.SetFlame(2);
        flamesOn = hotFlame.activeSelf && bunsenScript.flameState == 2 && !coolFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //4. set to invalid number, should ignore
        Assert.IsTrue(!(bunsenScript.SetFlame(-1)));
        Assert.IsTrue(!(bunsenScript.SetFlame(1000)));
        Assert.IsTrue(!(bunsenScript.SetFlame(3)));

        yield return null;
    }

    [UnityTest]
    public IEnumerator ToggleFlameTests()
    {
        GameObject bunsen = GameObject.Find("BunsenBurner");
        Assert.IsTrue(bunsen != null);

        var bunsenScript = bunsen.GetComponent<BunsenBurner>();
        Assert.IsTrue(bunsenScript != null);

        GameObject coolFlame = bunsen.transform.GetChild(0).gameObject;
        GameObject hotFlame = bunsen.transform.GetChild(1).gameObject;

        //check that the bunsen burner can toggle states
        //turn flame off
        bunsenScript.SetFlame(0);

        //check button exists on bunsen burner
        Button btn = bunsen.transform.Find("Canvas/Button").GetComponent<Button>();
        Assert.IsTrue(btn != null);

        //toggle (to cool flame)
        btn.onClick.Invoke();
        bool success = bunsenScript.flameState==1;
        bool flamesOn = coolFlame.activeSelf && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //toggle (to hot flame)
        btn.onClick.Invoke();
        success = bunsenScript.flameState == 2;
        flamesOn = hotFlame.activeSelf && !coolFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //toggle (to off)
        btn.onClick.Invoke();
        success = bunsenScript.flameState == 0;
        flamesOn = hotFlame.activeSelf || coolFlame.activeSelf;
        Assert.IsTrue(success && !flamesOn);

        //toggle (to cool flame) to ensure looping correctly
        btn.onClick.Invoke();
        success = bunsenScript.flameState == 1;
        flamesOn = coolFlame.activeSelf && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        yield return null;
    }

}