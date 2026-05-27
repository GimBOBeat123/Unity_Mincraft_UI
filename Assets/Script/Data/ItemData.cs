using UnityEngine;

[CreateAssetMenu(menuName = "Craft/Item")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite icon;
}