using UnityEngine;
/// <summary>
/// Item Scriptable Object that can store information about an item.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite thumbnail;
    public GameObject model;
    public bool isHazardous;
    public string hazardInfo;
}