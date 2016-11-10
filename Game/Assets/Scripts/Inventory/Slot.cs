using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int id;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();


            if (inv.items[id].ID == -1)
            {
                inv.items[droppedItem.slotID] = new Item();
                inv.items[id] = droppedItem.item;

                droppedItem.slotID = id;
            Debug.Log(inv.items[id].ID);
            }
            else
            {
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().slotID = droppedItem.slotID;
                item.transform.SetParent(inv.slots[droppedItem.slotID].transform);
                item.transform.position = inv.slots[droppedItem.slotID].transform.position;

                droppedItem.slotID = id;
                droppedItem.transform.SetParent(this.transform);
                droppedItem.transform.position = this.transform.position;

                inv.items[droppedItem.slotID] = item.GetComponent<ItemData>().item;
                inv.items[id] = droppedItem.item;
            Debug.Log(inv.items[id].ID);
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemData itemToConsume = eventData.pointerPress.transform.GetChild(0).GetComponent<ItemData>();

        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            Debug.Log(itemToConsume.slotID);
            if(itemToConsume.slotID != -1)
            {
                inv.UseConsumable(itemToConsume.item);
                Debug.Log("consumed item");
            }
        }
        if(eventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
