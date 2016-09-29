using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

    public float roamingSpeed;
    public float chaseSpeed;
    public float chaseWaitTime;
    public float roamingWaitTime;
    public Transform[] roamingPoints;

    private NavMeshAgent nav;
    private Transform player;

    private float roamTimer;
    private float chaseTimer;
    private int pointIndex;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    void Update()
    {
        
    }

    void MeleeAttack()
    {

    }

    void RangedAttck()
    {

    }

    void Chasing()
    {

    }

    void freeRoaming()
    {
        nav.speed = roamingSpeed;

       // if()
      //  {
       //     nav.destination = roamingPoints[pointIndex].position;
       // }
    }

 
}
