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
    private float oldhp = 0;

    [Header("Mana Bar")]
    public Image Manabar;
    public Text ManaText;

    [Header("Mana Settings")]
    public float Mana = 200;
    public float MaxMana = 200;
    private float oldmana = 0;
    public float ManaChargePerSecond = 1;
    private float ManaTimer = 0;

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

        if (Mana < MaxMana && ManaTimer < Time.time)
        {
            ManaTimer = Time.time + 1;
            RechargeMana();
        }
        if (oldmana != Mana || oldmana == 0)
        {
            if (ManaText) { ManaText.text = "Mana: " + Mana.ToString(); }
            if (Manabar) { Manabar.fillAmount = Mana / MaxMana; }
            
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

    public void RechargeMana()
    {
        if (Mana < MaxMana)
        {
            Mana += ManaChargePerSecond;
            if (Mana > MaxMana) { Mana = MaxMana; }
        }
    }
    public void ChangeMana(float amount)
    {
        Mana += amount;
    }

}
