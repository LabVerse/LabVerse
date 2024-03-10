using UnityEngine;
using TMPro;

public class Checklist : MonoBehaviour
{
    [SerializeField]
    private GameObject checklistItemPrefab;
    [SerializeField]
    private GameObject checklistContent;
    private bool isSequential;


    private void OnEnable()
    {
        ExperimentManager.startExperiment += InitializeChecklist;
        ExperimentManager.endExperimentStage += UpdateStage;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperiment -= InitializeChecklist;
    }

    private void InitializeChecklist()
    {
        isSequential = ExperimentManager.instance.selectedExperiment.areStagesSequential;
        if (!isSequential)
        {
            foreach (Stage stage in ExperimentManager.instance.selectedExperiment.stages)
            {
                AddChecklistItem(stage.title);
            }
        }
    }

    private void UpdateStage(int stageIndex)
    {
        tickChecklistItem(checklistContent.transform.GetChild(stageIndex).gameObject);
    }

    private void AddChecklistItem(string title)
    {
        GameObject newItem = Instantiate(checklistItemPrefab, checklistContent.transform);

        TMP_Text text = newItem.GetComponentInChildren<TMP_Text>();
        text.text = title;

    }
    private void tickChecklistItem(GameObject item)
    {
        item.transform.Find("Checkbox/Tick").gameObject.SetActive(true);
    }


}
