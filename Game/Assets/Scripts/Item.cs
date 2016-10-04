using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public string itemDisc;
    public Texture2D itemIcon;
    public int itemPower;
    public int itemSpeed;
    public ItemType itemType;

    public enum ItemType
    {
        weapon,
        Consumable,
        Quest,
        thrash
    }

    public Item(string name, int id, string disc, int power, int speed, ItemType type)
    {
        itemID = id;
        itemName = name;
        itemDisc = disc;
        itemPower = power;
        itemSpeed = speed;
        itemType = type;
    }
    public Item()
    {
    }
}
