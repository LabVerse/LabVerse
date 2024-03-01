using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NonSequentialExperimentManagerTests
{
    private bool startExperimentEventFired = false;
    private bool endExperimentEventFired = false;
    private bool startExperimentStageEventFired = false;
    private bool endExperimentStageEventFired = false;

    [SetUp]
    public void Setup()
    {
        // Initially starts at a automatically generated scene.

        PlayerPrefs.SetString("ExperimentName", "ExampleTestNonSequentialExperiment");

        EditorSceneManager.LoadSceneInPlayMode("Assets/Tests/Scenes/ExperimentManagerScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
        
        // Subscribe to events.
        ExperimentManager.startExperiment += () => { startExperimentEventFired = true; };
        ExperimentManager.endExperiment += () => { endExperimentEventFired = true; };
        ExperimentManager.startExperimentStage += (stageIndex) => { startExperimentStageEventFired = true; };
        ExperimentManager.endExperimentStage += (stageIndex) => { endExperimentStageEventFired = true; };
    }

    [UnityTest, Order(1)]
    public IEnumerator ExperimentStarts()
    {
        // Check if the experiment was stored in the PlayerPrefs.
        string experimentName = PlayerPrefs.GetString("ExperimentName");
        Assert.IsFalse(string.IsNullOrEmpty(experimentName));

        // Check if the ExperimentManager is present in the scene.
        ExperimentManager experimentManager = Object.FindObjectOfType<ExperimentManager>();
        Assert.IsNotNull(experimentManager);

        // Check if the startExperiment event was fired.
        Assert.IsTrue(startExperimentEventFired);
        // Reset the value.
        startExperimentEventFired = false;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator ValidExperimentChangeStage()
    {
        // Check if the ExperimentManager is present in the scene.
        StageManager stageManager = Object.FindObjectOfType<StageManager>();
        Assert.IsNotNull(stageManager);

        bool completed = true;
        // By default already in stage 0.
        stageManager.EnterStage(1);
        stageManager.FinishStage(1, completed);

        startExperimentStageEventFired = false;
        Assert.IsFalse(startExperimentStageEventFired);
        stageManager.EnterStage(2);
        Assert.IsTrue(startExperimentStageEventFired);

        endExperimentStageEventFired = false;
        Assert.IsFalse(endExperimentStageEventFired);
        stageManager.FinishStage(2, completed);
        Assert.IsTrue(endExperimentStageEventFired);

        Assert.IsFalse(endExperimentEventFired);
        stageManager.EnterStage(0);
        stageManager.FinishStage(0, completed);
        // Reached the last stage so the experiment should end.
        Assert.IsTrue(endExperimentEventFired);
        // Reset the value.
        endExperimentEventFired = false;

        yield return null;
    }
}