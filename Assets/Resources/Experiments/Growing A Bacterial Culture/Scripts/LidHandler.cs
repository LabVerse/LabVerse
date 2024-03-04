using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class LidHandler : MonoBehaviour
{
    [SerializeField] GameObject lid;
    public void OnLidClicked()
    {
        lid.SetActive(!lid.activeSelf);
    }
}
