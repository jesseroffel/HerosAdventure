using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public float speed = 1;
    public float rotationspeed;

    Quaternion targetrotation;

    public Animator anim;
    public Rigidbody rb;

    float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targetrotation; }
    }
    
	void Start ()
    {
        targetrotation = transform.rotation;
        forwardInput = turnInput = 0;
	}

    void Update()
    {
        GetInput();
        Turn();
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


}
