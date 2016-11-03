using UnityEngine;
using System.Collections;

public class PresurePlate : MonoBehaviour
{
    public GameObject target;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if(col.tag == "Player")
        {
            target.SendMessage("Action");
        }
    }
}
