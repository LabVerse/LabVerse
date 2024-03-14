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

    public Experiment selectedExperiment;

    [NonSerialized]
    public int currentStageIndex = 0;

    [SerializeField]
    private Experiment[] m_availableExperiments;

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
    
        if (!selectedExperiment) {
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
        startExperimentStage?.Invoke(currentStageIndex);
        // Delete this. Example of how to spawn alerts.
        AlertManager.instance.CreateAlert(AlertManager.ALERT_TYPE.INFO, "To access the quick menu, look at your palm of your hand.");
        AlertManager.instance.CreateAlert(AlertManager.ALERT_TYPE.INFO, "Tap the surface of your table to start the experiment.");
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

    /// <summary>
    /// Start the stage.
    /// </summary>
    private void StartStage(int stageIndex)
    {
        currentStageIndex = stageIndex;
        startExperimentStage?.Invoke(stageIndex);
    }

    /// <summary>
    /// Safely end the current stage if necessary.
    private void EndCurrentStage()
    {
        endExperimentStage?.Invoke(currentStageIndex);
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
        if (stageIndex < 0 || stageIndex >= selectedExperiment.stages.Count) return;

        bool sequentialAndNextStage = selectedExperiment.areStagesSequential && stageIndex == currentStageIndex + 1;
        bool notSequentialAndDifferentStage = !selectedExperiment.areStagesSequential && stageIndex != currentStageIndex;
        if ((m_stagesCompletedStatus[currentStageIndex] && sequentialAndNextStage) || notSequentialAndDifferentStage)
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


