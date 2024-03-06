using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Change preview image and experiment description depending on option selected
// Move start button to appear on selected option

public class ChooseExperiment : MonoBehaviour
{

    
    [SerializeField] private string[] OptionDescriptions;
    [SerializeField] private Sprite[] OptionImages;
    [SerializeField] private List<Button> ExperimentOptions;
    [SerializeField] private TextMeshProUGUI CurrentDescription;
    [SerializeField] private Image CurrentImage;
    [SerializeField] private Button StartButton;

    void Start()
    {
        for (int i = 0; i < ExperimentOptions.Count; i++)
        {
            int index = i;
            ExperimentOptions[i].onClick.AddListener(() => ChangeSelectedOption(index));
        }
    }

    public void ChangeSelectedOption(int index)
    {
        CurrentDescription.text = OptionDescriptions[index];
        CurrentImage.sprite = OptionImages[index];
        // Move start button onto selected option
        StartButton.transform.SetParent(ExperimentOptions[index].transform, false);
        // Set the name of chosen experiment in player prefs so ExperimentLab knows which to load
        string ExperimentName = ExperimentOptions[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("ExperimentName", ExperimentName);
    }

}
