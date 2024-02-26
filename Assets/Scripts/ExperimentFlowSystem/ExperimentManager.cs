using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Experiment Manager that manages the flow of the experiment.
/// </summary>
public class ExperimentManager : MonoBehaviour
{
    public static event Action<string> startExperiment; // string: experiment name
    public static event Action endExperiment;
    public static event Action<int> startExperimentStage; // int: stage index
    public static event Action<int> endExperimentStage; // int: stage index

    [SerializeField]
    private Experiment[] m_availableExperiments;

    [SerializeField]
    public Experiment m_experiment;

    private int m_currentStageIndex = 0;
    private bool[] m_stagesCompletedStatus;

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

        // Fill the array with false.
        m_stagesCompletedStatus = new bool[m_experiment.stages.Count];
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
        startExperiment?.Invoke(m_experiment.experimentName);
        startExperimentStage?.Invoke(m_currentStageIndex);
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
        startExperimentStage?.Invoke(stageIndex);

        // Other logic to start the stage.
    }

    /// <summary>
    /// Safely end the current stage if necessary.
    private void EndCurrentStage()
    {
        endExperimentStage?.Invoke(m_currentStageIndex);
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

        bool sequentialAndNextStage = m_experiment.areStagesSequential && stageIndex == m_currentStageIndex + 1;
        bool notSequentialAndDifferentStage = !m_experiment.areStagesSequential && stageIndex != m_currentStageIndex;
        if ((m_stagesCompletedStatus[m_currentStageIndex] && sequentialAndNextStage) || notSequentialAndDifferentStage)
        {
            StartStage(stageIndex);
        }
    }

    /// <summary>
    /// Store that the stage has been completed, and end the experiment if all stages have been completed or if the stage has failed.
    /// </summary>
    private void CompleteStage(int stageIndex, bool completed)
    {
        m_stagesCompletedStatus[stageIndex] = completed;
        bool completedAllStages = m_stagesCompletedStatus.All(stageComplete => stageComplete);
        if (!completed || completedAllStages)
        {
            EndCurrentStage();
            EndExperiment(completed);
        }
        else
        {
            EndCurrentStage();
        }
    }
}


