using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour {

    Text healthText;
    public float health = 100;
    public float maxHealth = 100;
    ChangeScene scene;
    public Transform resetPosition;

	void Start ()
    {
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        scene = GameObject.Find("GameHandler").GetComponent<ChangeScene>();
	}
	
    void Update()
    {
        if(health <= 1)
        {
            scene.ChangeToScene(2);
        }
        respawn();
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthText.text = "health: " + health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = "health: " + health.ToString();
    }

    public void respawn()
    {
        if(transform.position.y <= 70)
        {
           // transform.position = resetPosition.position;
        }
    }
	/*void OnTriggerEnter (Collider col)
    {
        Debug.Log(col.tag);
	    if(col.tag == "Enemy")
        {
            health -= 10;
            healthText.text = "health: " + health.ToString(); 
        }
	}*/
}
