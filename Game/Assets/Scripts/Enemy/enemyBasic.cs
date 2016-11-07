using UnityEngine;
using System.Collections;

public class enemyBasic : MonoBehaviour
{
    float distance;
    public Transform target;
    public float lookDistance;
    public float chaseDist;
    public float attackRange;
    public float speed;
    public int damage;
    public float damping;
    float timeStamp;
    public float coolDown;

    public Player_Health playerhealth;
	
	void Start()
    {
        if (target == null) { Debug.LogError("Enemy: " + gameObject.name + " has no Transform target!"); }
    }
	void Update ()
    {
        if (target)
        {
            distance = Vector3.Distance(target.position, transform.position);

            //Debug.Log(distance);
            if (distance < lookDistance)
            {
                LookAt();
            }
            if (distance < chaseDist)
            {
                Chase();
            }
            if(distance < attackRange)
            {
                if(timeStamp <= Time.time)
                {
                    Attack();
                    timeStamp = Time.time + coolDown;
                }
            }
        }

	}

    void LookAt()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void Chase()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Attack()
    {
        playerhealth.TakeDamage(damage);
    }
}
