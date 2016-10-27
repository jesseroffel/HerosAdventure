using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    void Action()
    {
        Debug.Log("Action Triggered");
        GameObject.Destroy(this.gameObject);
    }
}
