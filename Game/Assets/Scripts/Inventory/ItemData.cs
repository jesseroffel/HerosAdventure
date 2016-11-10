using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
//using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{

    public Item item;
    public int amount;
    public int slotID;

  //  private Transform originalParrent;
    private Inventory inv;
    private Tooltip tooltip;

    Transform quickSlot1;
    Transform quickSlot2;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        quickSlot1 = GameObject.Find("QuickSlotPanel").transform.GetChild(0);
        quickSlot2 = GameObject.Find("QuickSlotPanel").transform.GetChild(1);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item); 
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            
        }     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
