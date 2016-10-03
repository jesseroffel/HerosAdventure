using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("training Sword", 0, "a basic trainging sword", 1, 1, Item.ItemType.weapon));
        items.Add(new Item("potion", 1, "a basic health potion", 0, 0, Item.ItemType.Consumable));
    }

}
