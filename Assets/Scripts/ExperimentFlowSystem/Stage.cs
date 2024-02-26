using UnityEngine;

/// <summary>
/// Stage Scriptable Object that can store information about a stage.
/// </summary>
[CreateAssetMenu(fileName = "NewStage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    public string title;

    [TextArea]
    public string description;

    // other stage properties
}

