using UnityEngine;
using System.Collections;

public class FirstPersonControler : MonoBehaviour
{
    public float speed;

    Camera cam;

	void Start ()
    {
    //    Cursor.lockState = CursorLockMode.Locked;
     //   cam = GetComponentInChildren<Camera>();//GameObject.Find("Main camera").GetComponent<Camera>();
	}
	

	void Update ()
    {
        float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float sideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(sideward, 0, forward);


    }
}
