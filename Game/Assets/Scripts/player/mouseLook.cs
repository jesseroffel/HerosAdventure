using UnityEngine;
using System.Collections;

public class mouseLook : MonoBehaviour
{
    public GameObject player;
    public float mouseSensitivity;
    float rotX;
    float rotY;
    float curRotationX;
    float curRotationY;
    float mixRotY =0.5f;
    float maxRotY = -1;
    float smooting = 0.5f;


	void Start ()
    {
        rotY = 0;
    }
	

	void Update ()
    {
        rotX += Input.GetAxis("Mouse X") * mouseSensitivity/10;
        rotY -= Input.GetAxis("Mouse Y") * mouseSensitivity/10;

        if (rotY >= mixRotY)
        {
            rotY = mixRotY;
        }
        if (rotY <= maxRotY)
        {
            rotY = maxRotY;
        }

        transform.rotation = Quaternion.EulerAngles(rotY, rotX, 0);

        player.transform.rotation = Quaternion.EulerAngles(0, rotX, 0);
	}
}
