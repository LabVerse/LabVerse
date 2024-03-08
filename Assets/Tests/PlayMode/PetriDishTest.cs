using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class PetriDishTest
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Testing/Shawn/UnitTestScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest , Order(1)]
    public IEnumerator SetupPetriDish()
    {
        //Check if all components are present
        GameObject petriDish = GameObject.Find("petriDish");
        Assert.IsTrue(petriDish != null);

        PetriDishEventController petriDishEventController = petriDish.GetComponent<PetriDishEventController>();
        Assert.IsTrue(petriDishEventController != null);

        GameObject lid = petriDishEventController.lid;
        GameObject agarJelly = petriDishEventController.agarJelly;
        GameObject bacteria = petriDishEventController.bacteria;
        Assert.IsTrue(lid != null);
        Assert.IsTrue(agarJelly != null);
        Assert.IsTrue(bacteria != null);

        //Check if lid is active
        Assert.IsTrue(lid.activeSelf);

        //Check if agarJelly and bacteria are inactive
        Assert.IsFalse(agarJelly.activeSelf);
        Assert.IsFalse(bacteria.activeSelf);

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator GrowBacteria()
    {
        GameObject petriDish = GameObject.Find("petriDish");
        PetriDishEventController petriDishEventController = petriDish.GetComponent<PetriDishEventController>();
        GameObject lid = petriDishEventController.lid;
        GameObject agarJelly = petriDishEventController.agarJelly;
        GameObject bacteria = petriDishEventController.bacteria;
        BacteriaGrowthController bacteriaGrowthController = bacteria.GetComponent<BacteriaGrowthController>();

        //Check if bacteria grows when:
        //1. Lid is active, agarJelly is active and bacteria is active
        //Growth should occur
        SetActivity(lid, true, agarJelly, true, bacteria, true);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsTrue(child.activeSelf));

        //2. Lid is active, agarJelly is active and bacteria is inactive
        //No growth should occur
        SetActivity(lid, true, agarJelly, true, bacteria, false);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsTrue(child == null || !child.activeSelf));

        //3. Lid is active, agarJelly is inactive and bacteria is inactive
        //No growth should occur
        SetActivity(lid, true, agarJelly, false, bacteria, false);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsTrue(child == null || !child.activeSelf));

        //4. Lid is inactive, agarJelly is inactive and bacteria is inactive
        //No growth should occur
        SetActivity(lid, false, agarJelly, false, bacteria, false);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsTrue(child == null || !child.activeSelf));

        //5. Lid is inactive, agarJelly is active and bacteria is active
        //No growth should occur
        SetActivity(lid, false, agarJelly, true, bacteria, true);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsFalse(child.activeSelf));

        //6. Lid is inactive, agarJelly is inactive and bacteria is active
        //No growth should occur
        SetActivity(lid, false, agarJelly, false, bacteria, true);
        bacteriaGrowthController.GetBacteria().ForEach(child => Assert.IsFalse(child.activeSelf));

        yield return null;
    }

    private void SetActivity(GameObject lid, bool lidActive, GameObject agarJelly, bool agarJellyActive, GameObject bacteria, bool bacteriaActive)
    {
        lid.SetActive(lidActive);
        agarJelly.SetActive(agarJellyActive);
        bacteria.SetActive(bacteriaActive);
        Assert.IsTrue(lid.activeSelf == lidActive);
        Assert.IsTrue(agarJelly.activeSelf == agarJellyActive);
        Assert.IsTrue(bacteria.activeSelf == bacteriaActive);
    }
}
