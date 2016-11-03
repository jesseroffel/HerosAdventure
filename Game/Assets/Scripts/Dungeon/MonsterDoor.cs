using UnityEngine;
using System.Collections;

public class MonsterDoor : MonoBehaviour
{
    public EnemySpawn enemys;

	void Start ()
    {
	    
	}
	

	void Update ()
    {
	    if(!enemys.active)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
