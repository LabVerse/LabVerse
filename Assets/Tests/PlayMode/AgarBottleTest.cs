using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AgarBottleTest
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Testing/Shawn/UnitTestScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest, Order(1)]
    public IEnumerator SetupAgarBottle()
    {
        //Check if all components are present
        GameObject agarBottle = GameObject.Find("agarBottle");
        Assert.IsTrue(agarBottle != null);

        GameObject bottle = GameObject.Find("Bottle");
        Assert.IsTrue(bottle != null);

        PourHandler pourHandler = agarBottle.GetComponentInChildren<PourHandler>();
        Assert.IsTrue(pourHandler != null);

        GameObject cap = pourHandler.cap;
        GameObject agarFlow = pourHandler.agarFlow;
        Assert.IsTrue(cap != null);
        Assert.IsTrue(agarFlow != null);

        //Check if cap is active
        Assert.IsTrue(cap.activeSelf);

        //Check if agarFlow is inactive
        Assert.IsFalse(agarFlow.activeSelf);

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator PourAgar()
    {
        GameObject agarBottle = GameObject.Find("agarBottle");
        PourHandler pourHandler = agarBottle.GetComponentInChildren<PourHandler>();
        GameObject cap = pourHandler.cap;
        GameObject agarFlow = pourHandler.agarFlow;

        //Check if agar flow when:
        //Cap is active
        NoFlow(cap, agarBottle, agarFlow);

        //Cap is inactive
        Flow(cap, agarBottle, agarFlow);

        yield return null;
    }

    private void NoFlow(GameObject cap, GameObject agarBottle, GameObject agarFlow)
    {
        //Set cap active
        cap.SetActive(true);
        Assert.IsTrue(cap.activeSelf);

        //1. Cap is active, 0 rotation
        //No flow should occur
        SetActivity(agarBottle, 0, agarFlow, false);

        //2. Cap is active, 90 rotation
        //No flow should occur
        SetActivity(agarBottle, 90, agarFlow, false);

        //3. Cap is active, 180 rotation
        //No flow should occur
        SetActivity(agarBottle, 180, agarFlow, false);

        //4. Cap is active, 270 rotation
        //No flow should occur
        SetActivity(agarBottle, 270, agarFlow, false);

        //5. Cap is active, 360 rotation
        //No flow should occur
        SetActivity(agarBottle, 360, agarFlow, false);
    }

    private void Flow(GameObject cap, GameObject agarBottle, GameObject agarFlow)
    {
        //Set cap inactive
        cap.SetActive(false);
        Assert.IsFalse(cap.activeSelf);

        //1. Cap is active, 0 rotation
        //No flow should occur
        SetActivity(agarBottle, 0, agarFlow, false);

        //2. Cap is active, 90 rotation
        //No flow should occur
        SetActivity(agarBottle, 90, agarFlow, false);

        //3. Cap is active, 180 rotation
        //Flow should occur
        SetActivity(agarBottle, 180, agarFlow, true);

        //4. Cap is active, 270 rotation
        //No flow should occur
        SetActivity(agarBottle, 270, agarFlow, false);

        //5. Cap is active, 360 rotation
        //No flow should occur
        SetActivity(agarBottle, 360, agarFlow, false);
    }

    private IEnumerator SetActivity(GameObject agarBottle, float rotation, GameObject agarFlow, bool agarFlowActive)
    {
        agarBottle.transform.SetLocalPositionAndRotation(agarBottle.transform.position, Quaternion.Euler(rotation, 0, 0));

        yield return null;

        Assert.IsTrue(agarFlow.activeSelf == agarFlowActive);

        agarBottle.transform.SetLocalPositionAndRotation(agarBottle.transform.position, Quaternion.Euler(0, 0, 0));
    }
}
