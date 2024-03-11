using TMPro;
using UnityEngine;

/// <summary>
/// Logic to add explanation content of current stage to explanation menu.
/// </summary>
public class ExplanationMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_explanationDescription;

    private void OnEnable()
    {
        ExperimentManager.startExperimentStage += UpdateExplanation;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperimentStage -= UpdateExplanation;
    }

    private void UpdateExplanation(int stageIndex)
    {
        m_explanationDescription.text = ExperimentManager.instance.selectedExperiment.stages[stageIndex].explanation;
    }
}
