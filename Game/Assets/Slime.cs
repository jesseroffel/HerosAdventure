using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

    public Transform target;
    float targetDist;
    public float agroRange;
    public float attackRange;
    public float speed;
    public bool IsRanged;
    public float damping;

	void Start ()
    {
        if (target == null) { Debug.LogWarning("Enemy: Slime has no Target!!"); }
    }


    void FixedUpdate()
    {
        if (target != null)
        {
            targetDist = Vector3.Distance(target.position, transform.position);

            if (targetDist < agroRange && !IsRanged)
            {
                Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                if (IsRanged)
                {
                    RangedAttck();
                }
                else
                {
                    //Debug.Log("melee attack");
                    MeleeAttack();
                }
            }
        }
        
    }

    void MeleeAttack()
    {
        if(targetDist > attackRange)
        {
            //Debug.Log("attack");
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {

        }
    }

    void RangedAttck()
    {

    }

 
}
