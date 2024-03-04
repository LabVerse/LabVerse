using UnityEngine;
/// <summary>
/// Lid Handler for toggling petri dish lid visibility.
/// </summary>
public class LidHandler : MonoBehaviour
{
    [SerializeField] GameObject lid;
    /// <summary>
    /// OnCclick event for lid visibility toggle.
    /// </summary>
    public void OnLidClicked()
    {
        lid.SetActive(!lid.activeSelf);
    }
}
