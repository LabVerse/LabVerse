using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage Scriptable Object that can store information about a stage.
/// </summary>
[CreateAssetMenu(fileName = "NewStage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    public string title;

    [TextArea]
    public string description;

    // other stage properties
}

/// <summary>
/// Group of stages that can be used to organize the stages of an experiment.
/// </summary>
[CreateAssetMenu(fileName = "NewStageGroup", menuName = "StageGroup")]
public class StageGroup : ScriptableObject
{
    public List<Stage> stages = new List<Stage>();
    public bool m_areStagesSequential = false;
}

/// <summary>
/// Experiment Manager that manages the flow of the experiment.
/// </summary>
public class ExperimentManager : MonoBehaviour
{
    [SerializeField]
    private StageGroup stageGroup;

    private int m_currentStageIndex = 0;
    private bool m_currentStageComplete = false;

    private void Start()
    {
        StartExperiment();
    }

    private void OnEnable()
    {
        // Subscribe to events.
        StageHandler.enterStage += ChangeStage;
        StageHandler.finishStage += FinishStage;
    }

    private void OnDisable()
    {
        // Unsubscribe from events.
        StageHandler.enterStage -= ChangeStage;
        StageHandler.finishStage -= FinishStage;
    }

    /// <summary>
    /// Initialize the experiment.
    /// </summary>
    private void StartExperiment()
    {

    }
    
    /// <summary>
    /// Safely destroy the experiment.
    /// </summary>
    private void EndExperiment(bool completed)
    {
    }

    /// <summary>
    /// Start the stage.
    /// </summary>
    private void StartStage(int stageIndex)
    {
        m_currentStageIndex = stageIndex;

        // Other logic to start the stage.
    }

    /// <summary>
    /// Safely end the current stage.
    private void EndCurrentStage()
    {
        // Clean up the stage if necessary

        // Check if stage is completed
        if (m_currentStageComplete)
        {
            // 
        }
        else
        {
            EndExperiment(m_currentStageComplete);
        }
    }
    
    /// <summary>
    /// Changes the current stage to the specified stage index, if allowed.
    /// A change is allowed if the stages are sequential and the stage index is the next stage index,
    /// or if the stages are not sequential and the stage index is different from the current stage index.
    /// This is so that it can handle experiments that have stages that need to be completed in order,
    /// and experiments that have stages that can be completed in any order.
    /// </summary>
    private void ChangeStage(int stageIndex)
    {
        // Change stage
        if (stageIndex < 0 || stageIndex >= stageGroup.stages.Count)
        {
            return;
        }

        bool sequentialAndNextStage = stageGroup.m_areStagesSequential && stageIndex == m_currentStageIndex + 1;
        bool notSequentialAndDifferentStage = !stageGroup.m_areStagesSequential && stageIndex != m_currentStageIndex;
        if (sequentialAndNextStage || notSequentialAndDifferentStage)
        {
            EndCurrentStage();
            StartStage(stageIndex);
        }
    }

    /// <summary>
    /// Store that the stage has been completed
    /// </summary>
    private void FinishStage(int stageIndex, bool completed)
    {
        if (stageIndex == m_currentStageIndex)
        {
            if (completed)
            {
                m_currentStageComplete = completed;
            }
            else {
                // Stage failed
                EndExperiment(completed);
            }
        }
    }
}


