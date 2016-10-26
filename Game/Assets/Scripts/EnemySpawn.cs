using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    public GameObject SpawnObject;
    public Transform Target;
    private float WaitTime = 0;
    private bool CanSpawn = false;
    private bool check = true;

	void Start () {
        if (SpawnObject == null) { Debug.LogError("GameObject EnemyObject no has reference!"); }
        if (SpawnObject == null) { Debug.LogError("Transform Target no has reference!"); }
    }
	
	// Update is called once per frame
	void Update () {
        CheckForSpawn();

    }

    void CheckForSpawn()
    {
        if (gameObject.transform.childCount < 1 && check)
        {
            check = false;
            WaitTime = Time.time + 10.0f;
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
            GameObject Enemy = Instantiate(SpawnObject);
            Enemy.transform.position = gameObject.transform.position;
            Enemy.transform.parent = gameObject.transform;
            Enemy.GetComponent<enemyBasic>().target = Target;
        }
    }
}
