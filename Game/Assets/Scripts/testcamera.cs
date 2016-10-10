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
