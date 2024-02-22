using System;
using UnityEngine;

/// <summary>
/// Stage Manager with multiple events to handle stage changes.
/// </summary>
public class StageManager : MonoBehaviour
{
    public static event Action<int> enterStage;
    public static event Action<int, bool> finishStage;

    [SerializeField]
    private int m_stageIndex = 0;

    public int StageIndex
    {
        get { return m_stageIndex; }
        private set { m_stageIndex = value; }
    }

    /// <summary>
    /// Notify that the interaction for starting the specific stage has been triggered.
    /// </summary>
    public void EnterStage()
    {
        OnEnterStage();
    }

    /// <summary>
    /// Notify that the interaction for finishing (successfuly or not) the specific stage has been triggered.
    /// </summary>
    public void FinishStage(bool completed)
    {
        OnFinishStage(completed);
    }

    /// <summary>
    /// Invoke the enterStage event.
    /// </summary>
    private void OnEnterStage()
    {
        enterStage?.Invoke(m_stageIndex);
    }

    /// <summary>
    /// Invoke the finishStage event.
    /// </summary>
    private void OnFinishStage(bool completed)
    {
        finishStage?.Invoke(m_stageIndex, completed);
    }
}
