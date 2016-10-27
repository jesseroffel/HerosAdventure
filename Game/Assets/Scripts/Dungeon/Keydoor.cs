using UnityEngine;
using System.Collections;

public class Keydoor : MonoBehaviour
{
    public Inventory inventory;

    void Action()
    {
        Debug.Log("Action Triggered");
        GameObject.Destroy(this.gameObject);
    }

}
