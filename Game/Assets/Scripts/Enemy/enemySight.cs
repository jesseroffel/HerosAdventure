using UnityEngine;
using System.Collections;

public class enemySight : MonoBehaviour
{
    public float fieldOfView = 110f;
    public bool playerInsight;
    Vector3 lastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    //private LastPlayerSighting lps;
    private GameObject player;
    private Player_Health playerHealth;
    //private HashIDs hash;
    private Vector3 prevousSighting;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        // lps = Gameobject.FindGameObjectWithTag("gameController").Getcomponent<LastPlayerSighting();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player_Health>();
        //hash

        //lastSighting = prevousSightingw
    }



}
