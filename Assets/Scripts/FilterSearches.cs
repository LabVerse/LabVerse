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
       ExperimentOptions = new GameObject[NumOptions]; 
       for (int i = 0; i < NumOptions; i++)
       {
            ExperimentOptions[i] = ContentHolder.transform.GetChild(i).gameObject;
       }
    }

    public void Filter()
    {
        int VisibleOptionsCount = 0;
        string SearchText = SearchBar.GetComponent<TMP_InputField>().text;
        string OptionText;
        foreach (GameObject option in ExperimentOptions)
        {
            OptionText = option.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.ToLower();
            if (SearchText == "" || OptionText.Contains(SearchText.ToLower()))
            {
                option.SetActive(true);
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
