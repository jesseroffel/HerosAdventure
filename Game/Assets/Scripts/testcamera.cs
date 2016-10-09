using UnityEngine;
using System.Collections;

public class testcamera : MonoBehaviour {

    Transform player;
    public Transform lookAtTarget;
    public float rotateSpeed = 5;

    public float distanceX;
    public float distanceY;
    public float distanceZ;



    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.transform.position.x - distanceX, player.transform.position.y - distanceY, player.transform.position.z - distanceZ);
    }

    // Update is called once per frame
    void Update()
    {
         
         transform.LookAt(lookAtTarget);
         transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
    }
}
