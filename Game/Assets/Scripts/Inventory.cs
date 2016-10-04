using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int SlotY;
  //  public GUISkin skin;
    public List<Item> inventory = new List<Item>();

    private ItemDatabase database;
    private bool showInventory;

    private bool showToolTip;
    private string Tooltip;

	void Start ()
    {
	
	}
	

	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.I))        
        {
            Debug.Log("open inventory"); 
        }
	}

    void OnGui()
    {
        Tooltip = "";
       // GUI.skin = skin;
       if(showInventory)
        {
            DrawInventory();
        }
    }

    void DrawInventory()
    {
        int i;
        for(i = 0; i < inventory.Count; i++)
        {
            
        }
    }

    void UseConsumable()
    {

    }

    void RemoveItem()
    {

    }

    void AddItem()
    {

    }

    bool InventoryContains()
    {
        bool result = false;

        return result;
    }
}
