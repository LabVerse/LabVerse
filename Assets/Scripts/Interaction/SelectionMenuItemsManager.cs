using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuItemsManager : MonoBehaviour
{
    public static event Action<string> itemSelectionMenuItemClicked; // string: item name

    [NonSerialized]
    public Item[] items;

    [SerializeField]
    private GameObject m_itemToButtonPrefab;

    [SerializeField]
    private GameObject scrollMenuContainer;

    private void OnEnable()
    {
        Debug.Log("SelectionMenuItemsManager enabled");
        ExperimentManager.startExperiment += OnStartExperiment;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperiment -= OnStartExperiment;
    }

    private void OnStartExperiment()
    {
        items = ExperimentManager.instance.selectedExperiment.items;
        InstantiateItemsList(items);
    }

    private void InstantiateItemsList(Item[] models)
    {
        foreach (Item item in models) {
            GameObject itemToButton = Instantiate(m_itemToButtonPrefab, scrollMenuContainer.transform);
            itemToButton.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
            itemToButton.GetComponentInChildren<Button>().onClick.AddListener(() => itemSelectionMenuItemClicked?.Invoke(item.itemName));

            itemToButton.transform.localScale = new Vector3(itemToButton.transform.localScale.x, itemToButton.transform.localScale.y, 1f);
            itemToButton.transform.SetParent(scrollMenuContainer.transform);
        }
    }
}
