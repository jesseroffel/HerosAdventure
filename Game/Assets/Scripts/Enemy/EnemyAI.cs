using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;

    [Header("Enemy Combat")]
    public GameObject Projectile;
    public Transform SwordAttackPrefab;
    public Transform ArrowSpawn;
    public AudioClip[] CombatFxs;
    private AudioSource AudioSource;
    private int SoundSelector = 0;
    private float AudioVolume = 0.5f;

    public float BowStrengh = 1;
    private float ArrowSpeed = 1.5f;
    public float ShootCooldown = 2.0f;
    public float SlashCooldown = 2.0f;
    private float CooldownTime = 0;

    private bool CanAttack = false;
    public bool IsFrozen = false;

    private bool IsDead;
    private Transform spawnLocation;

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
        IsDead = false;
        spawnLocation = transform;
    }

    void Update()
    {
        if (!IsFrozen)
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
    }

    void MeleeAttack()
    {
        Debug.Log("melee attacking");
        nav.Stop();
        SwordAttack();
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
            if (nav.remainingDistance <= 0)
            {
                destination = (int)Random.Range(0.0f, 5.0f);
            }
            nav.destination = locations[destination].position;
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

    void SwordAttack()
    {
        if (CooldownTime < Time.time)
        {
			if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
            {
                SoundSelector = Random.Range(0, 3);
                AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
            }

            if (SwordAttackPrefab)
            {
                Transform EnemySwordAttack = (Transform)Instantiate(SwordAttackPrefab, transform.position + (transform.forward), transform.rotation);
                EnemySwordAttack.transform.parent = transform;
                EnemySwordAttack.transform.position += new Vector3(0, 0.4f, 0);
                EnemySwordAttack.GetComponent<HitRegistrator>().SetSettings(1, 0.75f, 10, transform.forward * 10, true);
            }
            



            CooldownTime = Time.time + SlashCooldown;
        }
    }

    void ShootArrow()
    {
        if (CooldownTime < Time.time)
        {
			if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
            {
                SoundSelector = Random.Range(3, 5);
                AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
            }

            GameObject Arrow = Instantiate(Projectile);
            Arrow.transform.position = ArrowSpawn.position;
            Arrow.transform.rotation = ArrowSpawn.rotation;
            Arrow.GetComponent<HitRegistrator>().SetSettings(2, 5, 10, transform.forward * ArrowSpeed,true);
            Rigidbody rb = Arrow.GetComponent<Rigidbody>();
            rb.velocity = (ArrowSpawn.forward * 50) * BowStrengh;

            

            CooldownTime = Time.time + ShootCooldown;
        }
    }

    void Respawn()
    {
        if(IsDead)
        {
            this.gameObject.transform.position = spawnLocation.position;
            this.gameObject.SetActive(true);
        }
    }
}
