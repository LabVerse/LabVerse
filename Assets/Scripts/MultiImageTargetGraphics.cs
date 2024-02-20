using UnityEngine;
using UnityEngine.UI;
 
public class MultiImageTargetGraphics : MonoBehaviour
{
    [SerializeField] private Image[] targetImages;
 
    public Image[] GetTargetImages => targetImages;
}