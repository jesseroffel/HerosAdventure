using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    [Header("Enemy Combat")]
    public GameObject Projectile;
    public Transform ArrowSpawn;
    public float BowStrengh = 1;
    private float ArrowSpeed = 1.5f;
    public float ShootCooldown = 2.0f;
    private float CooldownTime = 0;

    private bool CanAttack = false;

    [Header("Enemy Drop")]
    public GameObject drop;
    bool itemDropped;
    EnemyHP hp;

    [Header("Enemy Settings")]
    public float speed;
    public bool isRanged;
    float distance;

    [Header("AI Settings")]
    public float chaseRange;
    public float AttackRange;

    [Header("AI Roaming")]
    public Transform[] locations;
    int destination;

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
            CanAttack = true;
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
            if (drop)
            {
                GameObject.Instantiate(drop);
                drop.transform.position = transform.position;
            }
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
        if (distance >= AttackRange)
        {
            Chasing();
            CanAttack = false;
        }
        if (CanAttack) { ShootArrow(); }
        lookAt(player);
    }

    void Chasing()
    {
        nav.destination = player.position;
    }

    void freeRoaming()
    {
        if(locations.Length > 0)
        {
            nav.destination = locations[destination].position;

            if (nav.remainingDistance <= 0)
            {
                destination = (int)Random.Range(0.0f, 5.0f);
            }
        }
    }

    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
        nav.speed = newspeed;
    }

    void lookAt(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }

    void ShootArrow()
    {
        if (CooldownTime < Time.time)
        {
            GameObject Arrow = Instantiate(Projectile);
            Arrow.transform.position = ArrowSpawn.position;
            Arrow.transform.rotation = ArrowSpawn.rotation;
            Arrow.GetComponent<HitRegistrator>().SetSettings(2, 5, 10, transform.forward * ArrowSpeed,true);
            Rigidbody rb = Arrow.GetComponent<Rigidbody>();
            rb.velocity = (ArrowSpawn.forward * 50) * BowStrengh;

            

            CooldownTime = Time.time + ShootCooldown;
        }
    }
}
