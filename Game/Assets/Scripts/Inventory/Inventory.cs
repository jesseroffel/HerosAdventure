using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    Player_Health health;
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
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();

        for(int i=0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<Slot>().id = i;
        }

        AddItem(2);
        AddItem(2);
        AddItem(3);
        AddItem(3);

        inventoryPanel.active = false;  
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.active);
            Cursor.lockState = CursorLockMode.None;            
        }
        if(inventoryPanel.active)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);

        if(itemToAdd.Stackable && ItemInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slotID = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    
                    itemObj.name = itemToAdd.Title;

                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount = 1;

                    break;
                }
            }
        }
    }

    public void UseConsumable(Item itemToConsume)
    {
        if(itemToConsume.ID == 2)
        {
            Debug.Log("+ 10 health");
            health.health += itemToConsume.Power;
            RemoveItem(itemToConsume.ID);
        }
        if(itemToConsume.ID == 3)
        {
            Debug.Log("+ ten mana");
            RemoveItem(itemToConsume.ID);
        }
        RemoveItem(itemToConsume.ID);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        for(int i =0; i < items.Count; i++)
        {
            if(items[i].ID == itemToRemove.ID)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if(data.amount > 1)
                {
                    data.amount--;
                    Debug.Log("item amount -1");
                }
                else
                {
                    items[i] = new Item();
                    Debug.Log("item Removed");
                }                
            }
        }
    }

    public bool InventoryContains(int id)
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
    

    public bool ItemInInventory(Item item)
    {
        for(int i =0; i< items.Count; i++)
        {
            if(items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

    public bool ItemInInventoryInt(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)
            {
                return true;
            }
        }
        return false;
    }
}
