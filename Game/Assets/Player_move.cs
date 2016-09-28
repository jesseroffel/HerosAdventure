using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public Rigidbody rb;
    float forwardInput, sideInput;

    public camera_controler cam;
    public float speed = 1;
    float angle;

    public Animator anim;
  
	void Start ()
    {
     //   targetrotation = transform.rotation;
        forwardInput = sideInput = 0;
        if (cam == null) { Debug.LogError("Player has no camera target!"); }
	}

    void Update()
    {
        GetInput();
        //Turn();
     //   Orbit(transform, cam.transform, dir, speed);
    }

    void FixedUpdate()
    {
        Move();
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

    public float GetSpeed(){ return speed; }
    public void SetSpeed(float newspeed) { speed = newspeed; }
}
