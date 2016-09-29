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

    public void HitTarget(float damage, Vector3 force)
    {
        currentHP = currentHP - damage;
        if (currentHP > 0)
        {
            Debug.Log(gameObject.name + " damaged, HP:" + currentHP + " D: "+ damage + " MHP: " + MaxHP);
        }
        else
        {
            if (currentHP < 0) { currentHP = 0; }
            Debug.Log(gameObject.name + " defeated, HP: " + currentHP + "D: " + damage + "MHP: " + MaxHP);
            defeated = true;
        }
        transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
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
