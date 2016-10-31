using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    public GameObject EnemyGameObject;
    public Transform PlayerTarget;
    public Player_Health hp;
    public float RespawnTime = 5.0f;
    public bool active = true;
    private float WaitTime = 0;
    private bool CanSpawn = false;
    private bool check = true;


	void Start () {
        if (EnemyGameObject == null) { Debug.LogError("[ENEMYSPAWNER] GameObject EnemyGameObject no has reference!"); }
        if (PlayerTarget == null) { Debug.LogError("[ENEMYSPAWNER] Transform PlayerTarget no has reference!"); }
    }
	
	// Update is called once per frame
	void Update () {
        CheckForSpawn();

    }

    void CheckForSpawn()
    {
        if (active)
        {
            if (gameObject.transform.childCount < 1 && check)
            {
                check = false;
                WaitTime = Time.time + RespawnTime;
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
                if (EnemyGameObject)
                {
                    GameObject Enemy = Instantiate(EnemyGameObject);
                    Enemy.transform.position = gameObject.transform.position;
                    Enemy.transform.parent = gameObject.transform;
                    Enemy.GetComponent<enemyBasic>().target = PlayerTarget;
                    Enemy.GetComponent<enemyBasic>().playerhealth = hp;
                }
            } else
            {
                //active = false;
            }
        }
        
    }
}
