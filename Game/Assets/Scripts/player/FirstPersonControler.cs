using UnityEngine;
using System.Collections;

public class FirstPersonControler : MonoBehaviour
{
    public float speed;


	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	

	void Update ()
    {
        float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float sideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(sideward, 0, forward);
    }
}
