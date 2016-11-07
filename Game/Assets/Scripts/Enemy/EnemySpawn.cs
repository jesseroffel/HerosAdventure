using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    [Header("Enemy To Spawn")]
    public GameObject EnemyPrefab;
    [Header("Player Objects for Enemy")]
    public Transform PlayerTarget;
    public Player_Health hp;
    [Header("Spawner Settings")]
    public float RespawnTime = 5.0f;
    public int SpawnLimit = 0;              // SPAWNS AFTER KILL
    public int EnemyActiveLimit = 1;        // AMOUNT ACTIVE ENEMIES
    public float SpawnRadius = 1.0f;
    public bool UnlimitedSpawns = false;
    public bool RapidSpawn = false;
    public bool active = true;
    private float WaitTime = 0;
    private bool CanSpawn = false;
    private bool check = true;

    private int RadiusDistance = 0;
    private int CurrenRadius = 0;
    private int CurrentIndex = 1;
    private Vector3 Center;

	void Start () {
        if (EnemyPrefab == null) { Debug.LogError("[ENEMYSPAWNER] GameObject EnemyPrefab no has reference!"); }
        if (PlayerTarget == null) { Debug.LogError("[ENEMYSPAWNER] Transform PlayerTarget no has reference!"); }
        Center = transform.position;
        RadiusDistance = CalcDistance();
        if (SpawnLimit < EnemyActiveLimit) { SpawnLimit = EnemyActiveLimit; }
    }
	
	// Update is called once per frame
	void Update () {
        CheckForSpawn();

    }

    void CheckForSpawn()
    {
        if (active)
        {
            if (gameObject.transform.childCount < EnemyActiveLimit && check)
            {
                check = false;
                if (!RapidSpawn) { WaitTime = Time.time + RespawnTime; }
            }

            if (check == false)
            {
                if (Time.time > WaitTime)
                {
                    CanSpawn = true;
                }
            }

            if (CanSpawn)
            {
                CanSpawn = false;
                check = true;
                if (EnemyPrefab)
                {
                    if (EnemyActiveLimit != 1)
                    {
                        if (RadiusDistance < 1) { RadiusDistance = CalcDistance(); }

                        Vector3 Calcpos = CalcCircle(Center, SpawnRadius, CurrenRadius);
                        Spawn(Calcpos, Quaternion.identity);
                    } else
                    {
                        Spawn(transform.position, Quaternion.identity);
                    }
                   
                   

                    if (!UnlimitedSpawns) {
                        SpawnLimit--;
                        if (SpawnLimit == 0)
                        {
                            active = false;
                        }
                    }
                    
                }
            } else
            {
                //active = false;
            }
        }
        
    }

    

    void Spawn(Vector3 position, Quaternion quant)
    {

        GameObject Enemy = (GameObject)Instantiate(EnemyPrefab, position, quant);
        Enemy.transform.parent = gameObject.transform;
    }

    Vector3 CalcCircle(Vector3 center, float radius, int angle)
    {
        float ang = angle;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        if (CurrenRadius != 360) {
            CurrenRadius = CurrentIndex * RadiusDistance;
            CurrentIndex++;
        }
        return pos;
    }

    int CalcDistance()
    {
        int am = EnemyActiveLimit;
        int calc = 360 / am;
        return calc;
    }

    void Action()
    {
        active = true;
    }
}
