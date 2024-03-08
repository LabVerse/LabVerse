using UnityEngine;
/// <summary>
/// Parent Transform for easy access of euler rotation and scale from children scripts.
/// </summary>
public class ParentTransform : MonoBehaviour
{
    public Vector3 rotation;
    public Vector3 scale;
    // Start is called before the first frame update
    void Awake()
    {
        rotation = transform.localEulerAngles;
        scale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.localEulerAngles;
        scale = transform.lossyScale;
    }
}
