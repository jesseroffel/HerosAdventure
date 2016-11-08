using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour {
    [Header("Health Bar")]
    public Image healthbar;
    public ChangeScene scene;
    public Text healthText;

    [Header("Health Settings")]
    public float health = 100;
    public float maxHealth = 100;
    private int oldhp = 0;
    
    public Transform resetPosition;

	void Start ()
    {
        if (healthText == null) { healthText = GameObject.Find("healthText").GetComponent<Text>(); }
        if (scene = null) { scene = GameObject.Find("GameHandler").GetComponent<ChangeScene>(); }
        if (healthbar == null) { healthbar = GameObject.FindGameObjectWithTag("healthbar").GetComponent<Image>(); ; }
	}
	
    void Update()
    {
        if (oldhp != health|| oldhp == 0)
        {
            if (health <= 1)
            {
                scene.ChangeToScene(2);
            }
            respawn();
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            healthText.text = "Health: " + health.ToString();

            //healthbar.transform.localScale = new Vector3(health / 100, 1, 1);
            healthbar.fillAmount = health / maxHealth;
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthText) { healthText.text = "health: " + health.ToString(); }
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
