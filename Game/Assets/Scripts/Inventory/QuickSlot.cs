using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class QuickSlot : MonoBehaviour, IPointerClickHandler
{
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
}

