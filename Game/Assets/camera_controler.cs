using UnityEngine;
using System.Collections;

public class camera_controler : MonoBehaviour
{
    public GameObject target;
    public float orbitSpeed;

    public float maxDist;
    public float minDist;
    float distance;
    Vector3 offset;
    public float smooth;

    float y;

	void Start ()
    {
        offset = transform.position - target.transform.position;
    }
	

	void LateUpdate ()
    {
        FollowTarget();
        orbitCamera();
        transform.LookAt(target.transform);
    }

    void FollowTarget()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        

        if (distance > maxDist)
        {
            //follow
            transform.position = Vector3.Lerp(transform.position, target.transform.position, smooth) + offset;
            Debug.Log("folowing");
        }
        if (distance < minDist)
        {
            //back off
            Debug.Log("moving back");
        }
    }

    void orbitCamera()
    {

    }
}