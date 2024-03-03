// https://forum.unity.com/threads/tint-multiple-targets-with-single-button.279820/
// credit: CleverAI, the1whom0x

using UnityEngine;
using UnityEngine.UI;
 
public class MultiImageTargetGraphics : MonoBehaviour
{
    [SerializeField] private Graphic[] targetGraphics;
 
    public Graphic[] GetTargetGraphics => targetGraphics;
}