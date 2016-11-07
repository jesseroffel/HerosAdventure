using UnityEngine;
using System.Collections.Generic;

public class HitRegistrator : MonoBehaviour
{
    public int ProjectileType = 0;
    private float AliveTime = 0.0f;
    private float currentTime = 0.0f;
    private float DamageValue = 0.0f;
    private int MagicType = 0;
    
    private Vector3 PlayerForce;
    private Rigidbody rigid;
    private float StickTime = 0;
    private bool Active = true;
    private bool StickCheck = false;

    private List<int> GameobjectIDS = new List<int>();

    enum CombatType { NoCombat = 0, Melee = 1, Range = 2, Magic = 3 };

    public void SetSettings(int type, float alive, float damage, Vector3 playerf)
    {
        ProjectileType = type;
        AliveTime = alive;
        DamageValue = damage;
        PlayerForce = playerf;
        currentTime = Time.time + AliveTime;
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    public void SetSettings(int type, float alive, float damage, Vector3 playerf, int magictype)
    {
        ProjectileType = type;
        AliveTime = alive;
        DamageValue = damage;
        PlayerForce = playerf;
        currentTime = Time.time + AliveTime;
        rigid = gameObject.GetComponent<Rigidbody>();
        MagicType = magictype;
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

        if (ProjectileType == 3)
        {
            if (MagicType == 1)
            {
                MoveStraight();
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
                    bool check = CheckGameobjectIds(collision.gameObject.GetInstanceID());
                    if (!check)
                    {
                        switch (ProjectileType)
                        {
                            case 1:
                                collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
                                break;
                            case 2:
                                transform.parent = collision.gameObject.transform;
                                collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
                                break;
                            // SPELLS
                            case 3:
                                if (MagicType == 2)
                                {
                                    collision.gameObject.GetComponent<EnemyHP>().AddEffect(MagicType, 3);
                                    Debug.Log("[MAGIC] " + collision.gameObject.name + " Got Effect [SLOWNESS]");
                                }
                                break;
                        }
                        GameobjectIDS.Add(collision.gameObject.GetInstanceID());
                    }
                }
            } else
            {
                if (ProjectileType == (int)CombatType.Range)
                {
                    rigid.isKinematic = true;
                    StickTime = Time.time + 5;
                    StickCheck = true;
                    return;
                }
            }
        }
    }

    bool CheckGameobjectIds(int id)
    {
        // PLZ ADD ID CHECK
        return true;
    }

    void MoveStraight()
    {
        transform.position += transform.forward * 0.01f;
    }
}
