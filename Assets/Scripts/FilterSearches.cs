using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilterSearches : MonoBehaviour
{
    public GameObject SearchBar;
    public GameObject ContentHolder;
    public GameObject[] ExperimentOptions;
    public int NumOptions;

    void Start()
    {
        // Create a list of all the experiment options
        ExperimentOptions = new GameObject[NumOptions]; 
        for (int i = 0; i < NumOptions; i++)
        {
            ExperimentOptions[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
    }

    public void Filter()
    {
        // Display the options that contain the search query in their name
        int VisibleOptionsCount = 0;
        string SearchText = SearchBar.GetComponent<TMP_InputField>().text;
        string OptionText;
        foreach (GameObject option in ExperimentOptions)
        {
            OptionText = option.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.ToLower();
            if (OptionText.Contains(SearchText.ToLower()))
            {
                option.SetActive(true);
                // Move object so that it is at the top of the ScrollView with successive options below it
                option.transform.localPosition = new Vector3(30, -10-20*VisibleOptionsCount, 0);
                VisibleOptionsCount++;
            }
            else
            {
                option.SetActive(false);
            }
        }
    }
}
