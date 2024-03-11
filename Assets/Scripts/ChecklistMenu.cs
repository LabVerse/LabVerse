using UnityEngine;
using TMPro;

/// <summary>
/// Logic to add stage items to the checklist menu and update them when a stage is completed.
/// </summary>
public class ChecklistMenu : MonoBehaviour
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
        for (int i = 0; i < ExperimentManager.instance.selectedExperiment.stages.Count; i++)
        {
            Stage stage = ExperimentManager.instance.selectedExperiment.stages[i];
            AddChecklistItem(stage.title, stage.description);
            if (isSequential && i > 0)
            {
                checklistContent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void UpdateStage(int stageIndex)
    {
        tickChecklistItem(checklistContent.transform.GetChild(stageIndex).gameObject);
        if (checklistContent.transform.childCount > stageIndex + 1)
        {
            checklistContent.transform.GetChild(stageIndex + 1).gameObject.SetActive(true);
        }
    }

    private void AddChecklistItem(string title, string description)
    {
        GameObject newItem = Instantiate(checklistItemPrefab, checklistContent.transform);

        TMP_Text titleText = newItem.transform.Find("Title").GetComponent<TMP_Text>();
        TMP_Text descriptionText = newItem.transform.Find("Description").GetComponent<TMP_Text>();
        titleText.text = title;
        descriptionText.text = description;
        

    }
    private void tickChecklistItem(GameObject item)
    {
        item.transform.Find("Checkbox/Tick").gameObject.SetActive(true);
    }


}
