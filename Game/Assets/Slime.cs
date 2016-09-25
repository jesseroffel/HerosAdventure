using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

    public Transform target;
    float targetDist;
    public float agroRange;
    public float attackRange;
    public float speed;
    public int HP;
    public float damping;
    public bool IsRanged;


	void Start ()
    {
	    
	}


    void FixedUpdate()
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
                Debug.Log("melee attack");
                MeleeAttack();
            }
        }
    }

    void MeleeAttack()
    {
        if(targetDist > attackRange)
        {
            Debug.Log("attack");
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
