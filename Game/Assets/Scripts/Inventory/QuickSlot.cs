using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class QuickSlot : MonoBehaviour, IPointerClickHandler
{
    public int itemId;
    Inventory inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemData itemToConsume = eventData.pointerPress.GetComponent<ItemData>();
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemToConsume.slotID != -1)
            {
                Debug.Log("consumed item");
            }
        }
    }

    public void AddItemToSlot(int id)
    {
        for(int i = 0; i < inv.slots.Count; i++)
        {
            
        }
        Debug.Log(id);
    }
}

