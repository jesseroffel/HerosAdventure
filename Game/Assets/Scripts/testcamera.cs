using UnityEngine;
using System.Collections;

public class testcamera : MonoBehaviour {

    public Transform player;
    public Transform lookAtTarget;
    public float rotateSpeed = 5;

    public float distanceX;
    public float distanceY;
    public float distanceZ;

    void Start ()
    {       
     //   player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.transform.position.x - distanceX, player.transform.position.y - distanceY, player.transform.position.z - distanceZ);             
    }

    void Update()
    {       
        transform.LookAt(lookAtTarget);
        //Vector3 playerPos = new Vector3(player.transform.position.x - distanceX, player.transform.position.y - distanceY, player.transform.position.z - distanceZ);
        transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
        
        
    }
}

/*
public float turnSpeed = 4.0f;
public Transform player;

private Vector3 offset;

void Start()
{
    offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
}

void LateUpdate()
{
    offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
    transform.position = player.position + offset;
    transform.LookAt(player.position);
}*/