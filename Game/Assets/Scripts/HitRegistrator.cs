using UnityEngine;
using System.Collections;

public class HitRegistrator : MonoBehaviour
{
    public int ProjectileType = 0;
    private float AliveTime = 0.0f;
    private float currentTime = 0.0f;
    private float DamageValue = 0.0f;
    
    private Vector3 PlayerForce;
    private Rigidbody rigid;
    private float StickTime = 0;
    private bool Active = true;
    private bool StickCheck = false;
    
    

    public void SetSettings(int type, float alive, float damage, Vector3 playerf)
    {
        ProjectileType = type;
        AliveTime = alive;
        DamageValue = damage;
        PlayerForce = playerf;
        currentTime = Time.time + AliveTime;
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active && StickCheck == false)
        {
            if (Time.time > currentTime)
            {
                Destroy(gameObject);
            }
        }
        if (StickCheck)
        {
            if (StickTime < Time.time)
            {
                StickCheck = false;
                rigid.isKinematic = false;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
                currentTime = Time.time + AliveTime;
            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        collision.gameObject.GetComponent<EnemyHP>().GiveDamage(DamageValue);
     //   }
    //}

    void OnTriggerEnter(Collider collision)
    {
        if (Active && StickCheck == false)
        {
            if (collision.isTrigger)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    switch (ProjectileType)
                    {
                        case 1:
                            break;
                        case 2:
                            transform.parent = collision.gameObject.transform;
                            rigid.isKinematic = true;
                            StickTime = Time.time + 5;
                            StickCheck = true;
                            break;
                    }
                    collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
                }
            }
        }
    }
}
