using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Experiment Manager Singleton that manages the flow of the experiment.
/// </summary>
public class ExperimentManager : MonoBehaviour
{
    public static ExperimentManager instance { get; private set; }

    public static event Action startExperiment; // string: experiment name
    public static event Action endExperiment;
    public static event Action<int> startExperimentStage; // int: stage index
    public static event Action<int> endExperimentStage; // int: stage index

    [SerializeField]
    private Experiment[] m_availableExperiments;

    [SerializeField]
    public Experiment selectedExperiment;

    private int m_currentStageIndex = 0;
    private bool[] m_stagesCompletedStatus;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {   
        foreach (Experiment experiment in m_availableExperiments)
        {
            // PlayerPrefs stores Player preferences between game sessions.
            // Experiment name is stored in PlayerPrefs when the experiment is selected.
            if (experiment.experimentName == PlayerPrefs.GetString("ExperimentName"))
            {
                selectedExperiment = experiment;
                break;
            }
        }
    
        if (selectedExperiment == null) {
            Debug.LogError("No experiment selected or found");
            return;
        }

        // Fill the array with false.
        m_stagesCompletedStatus = new bool[selectedExperiment.stages.Count];
        StartExperiment();
    }

    private void OnEnable()
    {
        // Subscribe to events.
        StageManager.enterStage += ChangeStage;
        StageManager.finishStage += CompleteStage;
    }

    private void OnDisable()
    {
        // Unsubscribe from events.
        StageManager.enterStage -= ChangeStage;
        StageManager.finishStage -= CompleteStage;
    }

    /// <summary>
    /// Initialize the experiment.
    /// </summary>
    private void StartExperiment()
    {
        startExperiment?.Invoke();
        startExperimentStage?.Invoke(m_currentStageIndex);
    }
    
    /// <summary>
    /// Safely destroy the experiment.
    /// </summary>
    private void EndExperiment(bool completed)
    {
        endExperiment?.Invoke();
        PlayerPrefs.SetString("ExperimentCompletedStatus", completed ? "completed" : "not completed");
        SceneManager.LoadScene("ExperimentCompletionMenu");

        // TODO: Change to experiment completion scene.
    }

    public Item[] GetExperimentItems()
    {
        return selectedExperiment.items;
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
        if (stageIndex < 0 || stageIndex >= selectedExperiment.stages.Count)
        {
            return;
        }

        bool sequentialAndNextStage = selectedExperiment.areStagesSequential && stageIndex == m_currentStageIndex + 1;
        bool notSequentialAndDifferentStage = !selectedExperiment.areStagesSequential && stageIndex != m_currentStageIndex;
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


