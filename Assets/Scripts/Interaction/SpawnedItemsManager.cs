using System.Linq;
using UnityEngine;

/// <summary>
/// Manager of the ItemSpawner component.
/// </summary>
[RequireComponent(typeof(ItemSpawner))]
public class SpawnedItemsManager : MonoBehaviour
{
    private ItemSpawner m_Spawner;

    public void OnEnable()
    {
        m_Spawner = GetComponent<ItemSpawner>();
        m_Spawner.spawnAsChildren = true;
        ItemSelectionMenu.itemSelectionMenuItemClicked += UpdateSpawnerSelectedItem;
    }

    /// <summary>
    /// Handles the event of an item being selected in the item selection menu.
    /// </summary>
    public void UpdateSpawnerSelectedItem(string itemName)
    {
        foreach ((Item item, int i) in ExperimentManager.instance.selectedExperiment.items.Select((value, i) => (value, i)))
        {
            if (item.itemName == itemName)
            {
                m_Spawner.spawnOptionIndex = i;
                break;
            }
        }
    }
}
