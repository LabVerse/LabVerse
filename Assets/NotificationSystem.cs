using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    [NonSerialized]
    public Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartExperiment()
    {
        items = ExperimentManager.instance.selectedExperiment.items;
        AddItemHazardListener(items);
    }

    private void AddItemHazardListener(Item[] items)
    {
        foreach(Item item in items)
        {
            if(item.isHazardous)
            {
                //item.model.AddComponent;
            }
        }
    }
}
