using UnityEngine;
/// <summary>
/// Bacteria Precence for inoculating loop. Used by other scripts to ensure bacteria is present on gameobject before progressing through stage.
/// </summary>
public class BacteriaPresence : MonoBehaviour
{
    public bool bacteriaPresent = false;
    public GameObject bacteria;
}
