using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    public GameObject Projectile;

    public GameObject drop;
    bool itemDropped;
    EnemyHP hp;

    public Transform[] locations;
    int destination;

    public float speed;
    public bool isRanged;
    float distance;

    public float chaseRange;
    public float AttackRange;
    public float minDistance;

    bool chasing;
    bool attacking;
    bool roaming;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = GetComponent<EnemyHP>();
        nav.speed = speed;
        itemDropped = false;
    }

    void Update()
    {
        
        distance = Vector3.Distance(transform.position, player.transform.position);
       /// Debug.Log(distance);
        if (distance <= chaseRange)
        {
            Chasing();
        }
        else
        {
            freeRoaming();
        }

        if (distance <= AttackRange)
        {
            if (isRanged)
            {
                RangedAttack();
            }
            else
            {
                MeleeAttack();
            }
        }
        if (hp.defeated && !itemDropped)
        {
            GameObject.Instantiate(drop);
            drop.transform.position = transform.position;
            itemDropped = true;
        }
    }

    void MeleeAttack()
    {
        Debug.Log("melee attacking");
        nav.Stop();
    }

    void RangedAttack()
    {
        nav.Stop();
        Debug.Log("ranged attacking");
        if (distance <= minDistance)
        {
            Backoff();
        }
        if (distance >= AttackRange)
        {
            Chasing();
        }
    }

    void Chasing()
    {
        Debug.Log("chasing");
        nav.destination = player.position;
    }

    void freeRoaming()
    {
       // Debug.Log("free roaming");
        
        if(locations != null) nav.destination = locations[destination].position;

        if (nav.remainingDistance <= 0)
        {
            destination = (int)Random.Range(0.0f, 5.0f);
        }
    }

    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
        nav.speed = newspeed;
    }

    void Backoff()
    {
        Debug.Log("backing off");
        RaycastHit hit;
        Vector3 back = Vector3.back;

        if(!Physics.Raycast(transform.position, back, 5))
        {
            Debug.Log("nananan");
            nav.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
        }
    }
}
