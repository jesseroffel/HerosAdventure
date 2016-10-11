using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 20;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();



    void Start()
    {
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        database = GetComponent<ItemDatabase>();

        for(int i=0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);
        AddItem(1);
        AddItem(0);
        AddItem(0);
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        for(int i = 0; i< items.Count; i++)
        {
            if(items[i].ID == -1)
            {
                items[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.transform.SetParent(slots[i].transform);               
                itemObj.transform.position = Vector2.zero;
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = itemToAdd.Title;

                break;
            }
        }
    }

    void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        for(int i =0; i < items.Count; i++)
        {
            
        }

    }

    bool InventoryContains(int id)
	{
            bool result = false;
            for (int i = 0; i < items.Count; i++)
            {
                result = items[i].ID == id;
                if (result)
                {
                    break;
                }
            }

        return result;
     }
    

}
