using System;
using UnityEngine;

/// <summary>
/// Experiment Manager that manages the flow of the experiment.
/// </summary>
public class ExperimentManager : MonoBehaviour
{
    public static event Action startExperiment;
    public static event Action endExperiment;
    public static event Action<int> changeExperimentStage;

    [SerializeField]
    private Experiment[] m_availableExperiments;

    [SerializeField]
    private Experiment m_experiment;

    private int m_currentStageIndex = 0;
    private bool m_currentStageComplete = false;

    private void Start()
    {   
        foreach (Experiment experiment in m_availableExperiments)
        {
            // PlayerPrefs stores Player preferences between game sessions.
            // Experiment name is stored in PlayerPrefs when the experiment is selected.
            if (experiment.name == PlayerPrefs.GetString("ExperimentName"))
            {
                m_experiment = experiment;
                break;
            }
        }
    
        if (m_experiment == null) {
            Debug.LogError("No experiment selected or found");
            return;
        }

        StartExperiment();
    }

    private void OnEnable()
    {
        // Subscribe to events.
        StageHandler.enterStage += ChangeStage;
        StageHandler.finishStage += CompleteStage;
    }

    private void OnDisable()
    {
        // Unsubscribe from events.
        StageHandler.enterStage -= ChangeStage;
        StageHandler.finishStage -= CompleteStage;
    }

    /// <summary>
    /// Initialize the experiment.
    /// </summary>
    private void StartExperiment()
    {
        startExperiment?.Invoke();
    }
    
    /// <summary>
    /// Safely destroy the experiment.
    /// </summary>
    private void EndExperiment(bool completed)
    {
        endExperiment?.Invoke();
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
        if (stageIndex < 0 || stageIndex >= m_experiment.stages.Count)
        {
            return;
        }

        bool sequentialAndNextStage = m_experiment.m_areStagesSequential && stageIndex == m_currentStageIndex + 1;
        bool notSequentialAndDifferentStage = !m_experiment.m_areStagesSequential && stageIndex != m_currentStageIndex;
        if (sequentialAndNextStage || notSequentialAndDifferentStage)
        {
            EndCurrentStage();
            StartStage(stageIndex);
            changeExperimentStage?.Invoke(stageIndex);
        }
    }

    /// <summary>
    /// Store that the stage has been completed
    /// </summary>
    private void CompleteStage(int stageIndex, bool completed)
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


