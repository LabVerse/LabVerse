using System;
using UnityEngine;

public class SelectionMenuItemsListener : MonoBehaviour
{
    [NonSerialized]
    public Item item;

    private ItemSpawner itemSpawner;
    private GameObject inputPhoneManager;

    void Start()
    {
        itemSpawner = GameObject.Find("Object Spawner").GetComponent<ItemSpawner>();
        inputPhoneManager = GameObject.Find("Input Phone Manager");
    }
  

    public void OnObjectSelectedFromMenu()
    {
        //Debug.Log("Object selected from menu");
        //objectSpawner.m_objectPrefabs[0] = item;
        //inputPhoneManager.GetComponent<InputPhoneManager>().spawnablePrefab = item;
    }
}
