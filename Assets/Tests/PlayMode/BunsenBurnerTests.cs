using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class BunsenBurnerFlamesTests
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
        var bunsenScript = bunsen.GetComponent<BunsenBurnerFlames>();
        GameObject coolFlame = bunsen.transform.Find("BunsenFlame").GetChild(0).gameObject;
        GameObject hotFlame = bunsen.transform.Find("BunsenFlame").GetChild(1).gameObject;

        //check that the bunsen burner can switch between states as intended
        //1. turn flame off
        bool success = bunsenScript.SetFlame(BunsenBurnerFlames.FLAME_STATE.OFF);
        bool flamesOn = coolFlame.activeSelf || hotFlame.activeSelf || bunsenScript.IsLit();
        Assert.IsTrue(success && !flamesOn);

        //2. set to cool flame
        success = bunsenScript.SetFlame(BunsenBurnerFlames.FLAME_STATE.COOL);
        flamesOn = coolFlame.activeSelf && bunsenScript.GetFlameState()==BunsenBurnerFlames.FLAME_STATE.COOL && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //3. set to hot flame
        success = bunsenScript.SetFlame(BunsenBurnerFlames.FLAME_STATE.HOT);
        flamesOn = hotFlame.activeSelf && bunsenScript.GetFlameState() == BunsenBurnerFlames.FLAME_STATE.HOT && !coolFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //4. set to invalid number, should ignore
        //No longer possible using enums FLAME_STATES instead
        //Assert.IsTrue(!(bunsenScript.SetFlame(-1)));
        //Assert.IsTrue(!(bunsenScript.SetFlame(1000)));
        //Assert.IsTrue(!(bunsenScript.SetFlame(3)));

        yield return null;
    }

    [UnityTest]
    public IEnumerator ToggleFlameTests()
    {
        GameObject bunsen = GameObject.Find("BunsenBurner");
        Assert.IsTrue(bunsen != null);

        BunsenBurnerFlames bunsenScript = bunsen.GetComponent<BunsenBurnerFlames>();
        Assert.IsTrue(bunsenScript != null);

        GameObject coolFlame = bunsen.transform.Find("BunsenFlame").GetChild(0).gameObject;
        GameObject hotFlame = bunsen.transform.Find("BunsenFlame").GetChild(1).gameObject;

        //check that the bunsen burner can toggle states
        //turn flame off
        bunsenScript.SetFlame(BunsenBurnerFlames.FLAME_STATE.OFF);

        //check button exists on bunsen burner
        Button toggleFlameButton = bunsen.transform.Find("Canvas/Button").GetComponent<Button>();
        Assert.IsTrue(toggleFlameButton != null);

        //toggle (to cool flame)
        toggleFlameButton.onClick.Invoke();
        bool success = bunsenScript.GetFlameState() == BunsenBurnerFlames.FLAME_STATE.COOL;
        bool flamesOn = coolFlame.activeSelf && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //toggle (to hot flame)
        toggleFlameButton.onClick.Invoke();
        success = bunsenScript.GetFlameState() == BunsenBurnerFlames.FLAME_STATE.HOT;
        flamesOn = hotFlame.activeSelf && !coolFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        //toggle (to off)
        toggleFlameButton.onClick.Invoke();
        success = bunsenScript.GetFlameState() == 0;
        flamesOn = hotFlame.activeSelf || coolFlame.activeSelf;
        Assert.IsTrue(success && !flamesOn);

        //toggle (to cool flame) to ensure looping correctly
        toggleFlameButton.onClick.Invoke();
        success = bunsenScript.GetFlameState() == BunsenBurnerFlames.FLAME_STATE.COOL;
        flamesOn = coolFlame.activeSelf && !hotFlame.activeSelf;
        Assert.IsTrue(success && flamesOn);

        yield return null;
    }

}