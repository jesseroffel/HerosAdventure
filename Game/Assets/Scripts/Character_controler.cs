using UnityEngine;
using System.Collections;

public class Character_controler : MonoBehaviour
{
    public float speed = 5;
    public float jumpVelocitie = 5;
    public float mouseRotation = 5;
	public float maxSlope = 60;

    public GameObject camera;
	public Rigidbody rb;

    private bool grounded = false;

	void Start ()
    {
		rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate ()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        transform.Rotate(0, (Input.GetAxis("Mouse X") * mouseRotation * Time.deltaTime), 0);
        camera.transform.Rotate((Input.GetAxis("Mouse Y") * -mouseRotation * Time.deltaTime), 0, 0);

        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
			rb.AddForce(0,jumpVelocitie,0);
        }
    }

	void OnCollisionStay(Collision collisionInfo)
	{
		foreach(ContactPoint contacts in collisionInfo.contacts)
		{
			if(Vector3.Angle(contacts.normal, Vector3.up)< maxSlope)
			{
				grounded = true;
			}
		}
	}

	void OnCollisionExit()
	{
		grounded = false;
	}
}
