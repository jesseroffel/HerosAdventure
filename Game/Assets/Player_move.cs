using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public Rigidbody rb;
    public camera_controler cam;
    public Animator anim;
    public float speed = 1;
    public bool CanMove;

    float forwardInput, sideInput;
    float angle;

    Vector3 lastPosition;
  
	void Start ()
    {
        forwardInput = sideInput = 0;
        if (cam == null) { Debug.LogError("Player has no camera target!"); }
	}

    void Update()
    {
        GetInput();

        if (lastPosition == transform.position)
        {
            anim.SetFloat("speed", 0);
        }
        lastPosition = transform.position;
       // Debug.Log("LastPosition is: " + lastPosition);
       // Debug.Log("transform.position is: " + transform.position);
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
             Move();
        }      
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        sideInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        if (cam)
        {
            transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);

            if (sideInput < 0)
            {
                angle = -1;
                transform.RotateAround(cam.transform.position, new Vector3(0, 1, 0), angle);
            }
            if (sideInput > 0)
            {
                angle = 1;
                transform.RotateAround(cam.transform.position, new Vector3(0, 1, 0), angle);
            }
            anim.SetFloat("speed", 2);
        }
    }

    void Jump()
    {

    }
}
