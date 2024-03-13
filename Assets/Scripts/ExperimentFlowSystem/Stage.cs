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

    [TextArea]
    public string explanation;

    // other stage properties
}

