using UnityEngine;
using System.Collections;

public class Keydoor : MonoBehaviour
{
    public Inventory inventory;
    public int keyID;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if (inventory.InventoryContains(keyID))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
