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
            if(transform.position == entrance.transform.position)
            {
                player.transform.position = exit.transform.position;
            }
            else
            {
                player.transform.position = entrance.transform.position;
            }
    }
}
