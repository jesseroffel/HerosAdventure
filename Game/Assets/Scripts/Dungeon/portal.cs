using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour
{
    public Transform entrance;
    public Transform exit;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if (transform.position == entrance.transform.position)
            {
                player.transform.position = exit.transform.position;
                player.transform.rotation = exit.transform.rotation;
            }
            else
            {
                player.transform.localPosition = entrance.transform.position;
                player.transform.rotation = exit.transform.rotation;
            }
        }
    }
}
