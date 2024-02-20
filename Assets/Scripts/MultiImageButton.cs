using UnityEngine;
using UnityEngine.UI;
 
[RequireComponent(typeof(MultiImageTargetGraphics))]
public class MultiImageButton : Button
{
    private Image[] targetImages;
 
    private MultiImageTargetGraphics targetGraphics;
 
    protected override void Start()
    {
        targetGraphics = GetComponent<MultiImageTargetGraphics>();
 
        targetImages = targetGraphics.GetTargetImages;
 
        base.Start();
    }
 
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (!targetGraphics) return;
 
        var targetColor =
            state == SelectionState.Disabled ? colors.disabledColor :
            state == SelectionState.Highlighted ? colors.highlightedColor :
            state == SelectionState.Normal ? colors.normalColor :
            state == SelectionState.Pressed ? colors.pressedColor :
            state == SelectionState.Selected ? colors.selectedColor : Color.white;
 
        foreach (var image in targetImages)
            image.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
    }
}