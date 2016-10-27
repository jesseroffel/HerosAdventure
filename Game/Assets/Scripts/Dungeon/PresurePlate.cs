using UnityEngine;
using System.Collections;

public class PresurePlate : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if(col.tag == "Player")
        {
            door.SendMessage("Action");
        }
    }
}
