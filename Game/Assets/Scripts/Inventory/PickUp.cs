using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    public int itemId;
    public Inventory inventory;

    void Start()
    {
     //   inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //inventory.AddItem(itemId);
            GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(itemId);
            Destroy(this.gameObject);
        }
    }
}
