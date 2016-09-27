using UnityEngine;
using System.Collections;

public class camera_controler : MonoBehaviour
{
    #region myshit
    /*
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

     }*/
    #endregion

    
    #region tutorial shit
    
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


    #endregion
}