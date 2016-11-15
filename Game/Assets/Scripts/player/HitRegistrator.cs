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
    private float ArrowFallSpeed = 1.0f;

    private bool EnemyShot = false;

    private List<int> GameobjectIDS = new List<int>();

    public Material[] Materials;
    private Renderer rend;

    // COLORS
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

    public void SetSettings(int type, float alive, float damage, Vector3 playerf, bool EnemyProjectile)
    {
        ProjectileType = type;
        AliveTime = alive;
        DamageValue = damage;
        PlayerForce = playerf;
        currentTime = Time.time + AliveTime;
        rigid = gameObject.GetComponent<Rigidbody>();
        EnemyShot = EnemyProjectile;
    }

    public void SetSettings(int type, Spell spell)
    {
        ProjectileType = type;
        AliveTime = spell.AliveTime;
        DamageValue = spell.Change;
        currentTime = Time.time + AliveTime;
        MagicType = spell.ID;

        rend = transform.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = Materials[MagicType];
    }

    public void SetSettings(int type, Spell spell, Vector3 playerf)
    {
        ProjectileType = type;
        AliveTime = spell.AliveTime;
        DamageValue = spell.Change;
        currentTime = Time.time + AliveTime;
        MagicType = spell.ID;
        PlayerForce = playerf;
        rend.sharedMaterial = Materials[MagicType];
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
        //if (ProjectileType == 2)
        //{
            //Debug.Log(transform.rotation.x);
            //Debug.Log(transform.eulerAngles.x + " " + transform.eulerAngles.y + " " + transform.eulerAngles.z);
            //if (transform.rotation.x != 90)
            //{
                //transform.Rotate(new Vector3(-100 * Time.deltaTime, 0, 0 ));
            //}
        //}
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
                if (collision.gameObject.tag == "Enemy" || EnemyShot == true && collision.gameObject.tag == "Player")
                {
                    bool check = CheckGameobjectIds(collision.gameObject.GetInstanceID());
                    if (!check)
                    {
                        switch (ProjectileType)
                        {
                            case 1:
                                if (EnemyShot) {
                                    collision.gameObject.GetComponent<Player_Health>().HitPlayer(DamageValue, PlayerForce);
                                }
                                else
                                {
                                    collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
                                }
                                break;
                            case 2:
                                if (EnemyShot)
                                {
                                    collision.gameObject.GetComponent<Player_Health>().HitPlayer(DamageValue, PlayerForce);
                                } else
                                {
                                    //transform.parent = collision.gameObject.transform;
                                    collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
                                }
                                break;
                            // SPELLS
                            case 3:

                                switch (MagicType)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        collision.gameObject.GetComponent<EnemyHP>().AddEffect(MagicType, 10);
                                        Debug.Log("[MAGIC] " + collision.gameObject.name + " Got Effect [SLOWNESS]");
                                        break;
                                    case 3:
                                        collision.gameObject.GetComponent<EnemyHP>().AddEffect(MagicType, 10);
                                        Debug.Log("[MAGIC] " + collision.gameObject.name + " Got Effect [POISON]");
                                        //posion
                                        break;
                                    case 4:
                                        collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, Vector3.zero);
                                        collision.gameObject.GetComponent<EnemyHP>().AddEffect(MagicType, 10);
                                        
                                        Debug.Log("[MAGIC] " + collision.gameObject.name + " Got Effect [BURN]");
                                        break;
                                }
                                  
                                break;
                        }
                    }
                }
            } else
            {
                if (ProjectileType == (int)CombatType.Range)
                {
                    if (!EnemyShot)
                    {
                        rigid.isKinematic = true;
                        StickTime = Time.time + 5;
                        StickCheck = true;
                        return;
                    }
                }
            }
        }
    }

    bool CheckGameobjectIds(int id)
    {
        foreach (int obj in GameobjectIDS)
        {
            if (obj == id)
            {
                return true;
            }
        }
        GameobjectIDS.Add(id);
        return false;
    }

    void MoveStraight()
    {
        transform.position += transform.forward * 0.01f;
    }
}
