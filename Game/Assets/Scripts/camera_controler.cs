using UnityEngine;
using System.Collections;

public class camera_controler : MonoBehaviour
{  
    public float distance;
    public float upDistance;
    public float smooth;
    public Transform target;

    Vector3 targetPos;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        targetPos = target.position + target.up * upDistance - target.forward * distance;
        transform.position = Vector3.Lerp(transform.position, targetPos,Time.deltaTime * smooth);

        transform.LookAt(target);
    }

}