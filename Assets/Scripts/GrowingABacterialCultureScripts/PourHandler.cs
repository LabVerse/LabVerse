using UnityEngine;
/// <summary>
/// Pour handler that controls agar jelly flow from agar bottle.
/// </summary>
public class PourHandler : MonoBehaviour
{
    public GameObject cap;
    public GameObject agarFlow;
    [SerializeField] Transform parentTransform;


    /// <summary>
    /// Check if agar bottle is tilted and cap is off and enable agar flow gameobject.
    /// </summary>
    void Update()
    {
        if (cap.activeSelf) return;
        float rotationX = Mathf.Abs(parentTransform.localEulerAngles.x);
        float rotationZ = Mathf.Abs(parentTransform.localEulerAngles.z);
        if (rotationX > 180) rotationX = 360 - rotationX;
        if (rotationZ > 180) rotationZ = 360 - rotationZ;

        if (rotationX > 90 || rotationZ > 90)
        {
            agarFlow.SetActive(true);
        }
        else
        {             
            agarFlow.SetActive(false);
        }
    }
    /// <summary>
    /// OnClick event for removing and placing agar bottle cap.
    /// </summary>
    public void OnCapClicked()
    {
        cap.SetActive(!cap.activeSelf);
    }
}
