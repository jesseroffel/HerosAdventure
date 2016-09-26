using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public float speed = 1;
    bool CanJump;
    bool isWalking;

    public Animator anim;
    public Rigidbody RB;

	void Start ()
    {
        
	}
	

	void FixedUpdate ()
    {
	    if(Input.GetKey(KeyCode.W))
        {
            isWalking = true;
           // transform.Translate(Vector3.forward * speed * Time.deltaTime);
            anim.SetFloat("speed", 1.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }


        if(RB.velocity.magnitude == 0.0)
        {
            anim.SetFloat("speed", 0.0f);
        }
    }
}
