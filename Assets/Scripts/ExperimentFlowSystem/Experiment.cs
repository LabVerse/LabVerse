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
    public bool m_areStagesSequential = false;
}