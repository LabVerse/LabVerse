using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ItemSpawner))]
public class SpawnedItemsManager : MonoBehaviour
{
    private ItemSpawner m_Spawner;

    public void OnEnable()
    {
        m_Spawner = GetComponent<ItemSpawner>();
        m_Spawner.spawnAsChildren = true;
        SelectionMenuItemsManager.itemSelectionMenuItemClicked += OnItemSelectionMenuItemClicked;
    }

    public void OnItemSelectionMenuItemClicked(string itemName)
    {
        foreach ((Item item, int i) in ExperimentManager.instance.selectedExperiment.items.Select((value, i) => (value, i)))
        {
            if (item.itemName == itemName)
            {
                m_Spawner.spawnOptionIndex = i;
                break;
            }
        }

        Debug.Log("OnItemSelectionMenuItemClicked: " + m_Spawner.spawnOptionIndex.ToString());
    }
}
