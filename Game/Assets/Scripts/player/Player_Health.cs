using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour {

    Text healthText;
    float health = 100;

	void Start ()
    {
        healthText = GameObject.Find("healthText").GetComponent<Text>();
	}
	

	void OnTriggerEnter (Collider col)
    {
	    if(col.tag == "Enemy")
        {
            health -= 10;
            healthText.text = "health: " + health.ToString(); 
        }
	}
}
