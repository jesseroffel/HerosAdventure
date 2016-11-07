using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;

    public Transform[] locations;
    int destination;

    public float speed;
    public bool isRanged;
    float distance;

    public float chaseRange;

    bool chasing;
    bool attacking;
    bool roaming;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav.speed = speed;
        
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= chaseRange)
        {
            Chasing();
            //Debug.Log("chasing");
        }
        else
        {
            //freeRoaming();
            //Debug.Log("stop chasing");
        }
    }

    void MeleeAttack()
    {

    }

    void RangedAttck()
    {

    }

    void Chasing()
    {
        nav.destination = player.position;
    }

    void freeRoaming()
    {
        nav.destination = locations[destination].position;

        if (nav.remainingDistance <= 0)
        {
            destination = (int)Random.Range(0.0f, 5.0f);
            Debug.Log(destination);
        }
    }

 
}
