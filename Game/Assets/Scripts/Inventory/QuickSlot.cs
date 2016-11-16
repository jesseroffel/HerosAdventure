using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class QuickSlot : MonoBehaviour
{
    public int itemId;
    public string input;
    Inventory inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            Quickslot();
        }
    }

    public void Quickslot()
    {
        if(inv.ItemInInventoryInt(itemId))
        { 
             for(int i = 0; i < inv.items.Count; i++)
             {
                 if(inv.items[i].ID == itemId)
                 {
                     inv.UseConsumable(inv.items[i]);
                 }
             }
        }        
    }
}

