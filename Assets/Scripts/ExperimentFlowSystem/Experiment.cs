using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Group of stages that can be used to organize the stages of an experiment.
/// </summary>
[CreateAssetMenu(fileName = "NewExperiment", menuName = "Experiment")]
public class Experiment : ScriptableObject
{
    public string experimentName;
    public List<Stage> stages = new List<Stage>();
    public bool areStagesSequential = false;
    public Item[] items;

    // Prefab containing the combined items of the experiment arranged relative to each other appropriately
    public GameObject combinedItems;
}