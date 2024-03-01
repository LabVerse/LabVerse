using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic related to the item selection menu.
/// </summary>
public class ItemSelectionMenu : MonoBehaviour
{
    public static event Action<string> itemSelectionMenuItemClicked; // string: item name

    [NonSerialized]
    public Item[] items;

    [SerializeField]
    private GameObject m_itemToButtonPrefab;

    [SerializeField]
    private GameObject scrollMenuContainer;

    [SerializeField]
    private GameObject cardRoot;

    [SerializeField]
    private ItemSpawner m_itemSpawner;

    private void OnEnable()
    {
        Debug.Log("SelectionMenuItemsManager enabled");
        ExperimentManager.startExperiment += OnStartExperiment;
        m_itemSpawner.objectSpawned += OnObjectSpawned;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperiment -= OnStartExperiment;
        m_itemSpawner.objectSpawned -= OnObjectSpawned;
    }

    private void OnStartExperiment()
    {
        items = ExperimentManager.instance.selectedExperiment.items;
        InstantiateItemsList(items);
    }

    /// <summary>
    /// Instantiates the items listed in the item selection menu.
    /// </summary>
    private void InstantiateItemsList(Item[] models)
    {
        foreach (Item item in models) {
            GameObject itemToButton = Instantiate(m_itemToButtonPrefab, scrollMenuContainer.transform);

            // Update the button's text and add a listener to it
            itemToButton.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
            itemToButton.GetComponentInChildren<Button>().onClick.AddListener(() => itemSelectionMenuItemClicked?.Invoke(item.itemName));

            // Set the button's parent and scale
            itemToButton.transform.localScale = new Vector3(itemToButton.transform.localScale.x, itemToButton.transform.localScale.y, 1f);
            itemToButton.transform.SetParent(scrollMenuContainer.transform);
        }
    }

    /// <summary>
    /// Hides the item selection menu when an object is spawned.
    /// </summary>
    private void OnObjectSpawned(GameObject obj)
    {
        cardRoot.SetActive(false);
        m_itemSpawner.spawnOptionIndex = -1;
    }
}
