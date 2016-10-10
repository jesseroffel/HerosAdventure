using UnityEngine;
using System.Collections;

public class testcamera : MonoBehaviour {

    public Transform player;
    public Transform lookAtTarget;
    public float rotateSpeed = 5;

    public float distanceX;
    public float distanceY;
    public float distanceZ;

    public float followDist = 5;
    float dist;

    void Start ()
    {
       dist = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(followDist);
     //   player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.transform.position.x - distanceX, player.transform.position.y - distanceY, player.transform.position.z - distanceZ);
             
    }

    void Update()
    {
        if(dist > followDist)
        {
            transform.Translate(new Vector3(player.transform.position.x - distanceX, player.transform.position.y - distanceY, player.transform.position.z - distanceZ));
        }

         transform.LookAt(lookAtTarget);
         transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
    }
}
