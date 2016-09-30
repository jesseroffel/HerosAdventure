using UnityEngine;
using System.Collections;

public class enemyBasic : MonoBehaviour
{
    float distance;
    public Transform target;
    public float lookDistance;
    public float attackDist;
    public float speed;
    public float damping;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(target.position, transform.position);
        
        Debug.Log(distance);
        if(distance < lookDistance)
        {
            LookAt();
        }
        if(distance < attackDist)
        {
            Attack();
        }
	}

    void LookAt()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void Attack()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
