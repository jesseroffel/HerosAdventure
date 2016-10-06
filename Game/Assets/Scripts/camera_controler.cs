using UnityEngine;
using System.Collections;

public class camera_controler : MonoBehaviour
{  
    public float distance;
    public float upDistance;
    public float smooth;
    public Transform lookAtTarget;

    Vector3 targetPos;

    void Start()
    {
        if (lookAtTarget == null) { Debug.LogError("Transform: lookAtTarget is missing its reference"); }
    }

    void LateUpdate()
    {
        if (lookAtTarget)
        {
            targetPos = lookAtTarget.position + lookAtTarget.up * upDistance - lookAtTarget.forward * distance;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smooth);

            transform.LookAt(lookAtTarget);
        }
    }
}