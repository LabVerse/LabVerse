using System;
using UnityEngine;

/// <summary>
/// Stage Manager singleton with events to notify of potential stage changes.
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager instance { get; private set; }

    public static event Action<int> enterStage;
    public static event Action<int, bool> finishStage;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Notify that the interaction for starting the specific stage has been triggered.
    /// </summary>
    public void EnterStage(int stageIndex)
    {
        OnEnterStage(stageIndex);
    }

    /// <summary>
    /// Notify that the interaction for finishing (successfuly or not) the specific stage has been triggered.
    /// </summary>
    public void FinishStage(int stageIndex, bool completed)
    {
        OnFinishStage(stageIndex, completed);
    }

    /// <summary>
    /// Invoke the enterStage event.
    /// </summary>
    private void OnEnterStage(int stageIndex)
    {
        enterStage?.Invoke(stageIndex);
    }

    /// <summary>
    /// Invoke the finishStage event.
    /// </summary>
    private void OnFinishStage(int stageIndex, bool completed)
    {
        finishStage?.Invoke(stageIndex, completed);
    }
}
