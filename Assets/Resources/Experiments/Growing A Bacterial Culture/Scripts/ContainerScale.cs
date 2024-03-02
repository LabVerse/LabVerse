using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScale : MonoBehaviour
{
    public Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.lossyScale;
    }
}
