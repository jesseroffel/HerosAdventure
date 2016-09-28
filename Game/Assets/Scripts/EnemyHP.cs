using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {

    public float currentHP;
    private float MaxHP;
    private bool defeated = false;
    private int Type = 1;

    // Use this for initialization
    void Start () {
        if (MaxHP <= 0) {
            if (Type == 1)
            {
                MaxHP = 100;
            }
        }
        currentHP = MaxHP;
    }
	
	// Update is called once per frame
	void Update () {
	    if (defeated)
        {
            shrinkobject();
        }
	}

    public void GiveDamage(float damage)
    {
        float curval = currentHP;
        currentHP = currentHP - damage;
        if (currentHP > 0)
        {
            Debug.Log(gameObject.name + " took damage, old hp: " + curval + ", new hp: " + currentHP);
        }
        else
        {
            if (currentHP < 0) { currentHP = 0; }
            Debug.Log(gameObject.name + " defeated, old hp: " + curval + ", new hp: " + currentHP);
            defeated = true;
        }
    }

    void shrinkobject()
    {
        if (transform.localScale.x > 0.1f && transform.localScale.y > 0.1f && transform.localScale.z > 0.1f)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
