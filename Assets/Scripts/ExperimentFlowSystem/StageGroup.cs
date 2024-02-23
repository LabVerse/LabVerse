using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Group of stages that can be used to organize the stages of an experiment.
/// </summary>
[CreateAssetMenu(fileName = "NewStageGroup", menuName = "StageGroup")]
public class StageGroup : ScriptableObject
{
    public List<Stage> stages = new List<Stage>();
    public bool m_areStagesSequential = false;
}