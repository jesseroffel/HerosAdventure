using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class playertest : MonoBehaviour
{
    public float speed = 5;

    public float jumpVelocitie = 5;
    public float smooth;
    public float mouseRotation = 5;

    public Animator anim;
    public Rigidbody rb;
    public Camera camera;

    bool isMoving;
    bool canJumo;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(0, Vector3.up), smooth);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(180, Vector3.up), smooth);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(90, Vector3.up), smooth);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {          
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(270, Vector3.up), smooth);
            isMoving = true;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            anim.SetFloat("speed", 5);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }


       //  transform.Rotate(0, (Input.GetAxis("Mouse Y") * mouseRotation * Time.deltaTime), 0);
      //  camera.transform.Rotate((Input.GetAxis("Mouse Y") * -mouseRotation * Time.deltaTime), 0, 0);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }
    }
}