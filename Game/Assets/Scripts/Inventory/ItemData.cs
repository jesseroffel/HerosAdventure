using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Item item;
    public int amount;
    public int slotID;

  //  private Transform originalParrent;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
       this.transform.SetParent(inv.slots[slotID].transform);
        this.transform.position = inv.slots[slotID].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
