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


    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();

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
