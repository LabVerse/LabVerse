using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Change experiment image and description depending on option selected
// Move start button to appear on selected option

public class ChooseExperiment : MonoBehaviour
{

    public string[] OptionDescriptions;
    public Sprite[] OptionImages;
    public List<Button> ExperimentOptions;
    public string[] ExperimentScenes;
    public TextMeshProUGUI CurrentDescription;
    public Image CurrentImage;
    public Button StartButton;

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
        StartButton.transform.SetParent(ExperimentOptions[index].transform, false);
    }

}
