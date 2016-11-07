using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    public int itemId;
    Inventory inventory;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //inventory.AddItem(itemId);
            QuestList.QuestListObject.RegisterItemID(itemId);

            if (inventory == null) {
                GameObject.FindGameObjectWithTag("GameMasterObject").GetComponent<Inventory>().AddItem(itemId);
            } else {
                inventory.AddItem(itemId);
            }
            Destroy(this.gameObject);
        }
    }
}
