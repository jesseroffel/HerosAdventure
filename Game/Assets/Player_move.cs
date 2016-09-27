using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public float rotationspeed;
    Quaternion targetrotation;
    public Rigidbody rb;
    float forwardInput, turnInput;

    public camera_controler cam;
    public float speed = 1;
    float dir;
    float horizontal;
    float vertical;

    
	void Start ()
    {
        targetrotation = transform.rotation;
        forwardInput = turnInput = 0;
	}

    void Update()
    {
        GetInput();
        Turn();
        Orbit(transform, cam.transform, dir, speed);
    }

    void FixedUpdate()
    {
        Run();
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Run()
    {
        rb.velocity = transform.forward * forwardInput * speed;
    }

    void Turn()
    {
        targetrotation *= Quaternion.AngleAxis(rotationspeed * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetrotation;
    }

    void Orbit(Transform root, Transform cam, float dirOut, float speedOut)
    {
        Vector3 rootDir = root.forward;

        Vector3 moveDir = new Vector3(horizontal, 0, vertical);

        speedOut = moveDir.sqrMagnitude;
    }


}
