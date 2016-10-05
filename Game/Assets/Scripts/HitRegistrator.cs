using UnityEngine;
using System.Collections;

public class HitRegistrator : MonoBehaviour
{
    float AliveTime = 0.0f;
    float currentTime = 0.0f;
    float DamageValue = 0.0f;
    private Vector3 PlayerForce;


    public void SetSettings(float alive, float damage, Vector3 playerf)
    {
        AliveTime = alive;
        DamageValue = damage;
        PlayerForce = playerf;
        currentTime = Time.time + AliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > currentTime)
        {
            Destroy(gameObject);
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
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHP>().HitTarget(DamageValue, PlayerForce);
            }
        }

    }
}
