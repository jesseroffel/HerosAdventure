using UnityEngine;
using System.Collections;

public class HitRegistrator : MonoBehaviour
{
    float AliveTime = 0.3f;
    float currentTime = 0.0f;
    float DamageValue = 0.0f;
    // Use this for initialization
    void Start()
    {
        currentTime = Time.time + AliveTime;
    }

    public void Init(float alive, float damage)
    {
        AliveTime = alive;
        DamageValue = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > currentTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHP>().GiveDamage(DamageValue);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHP>().GiveDamage(DamageValue);
        }
    }
}
