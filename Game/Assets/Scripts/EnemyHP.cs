using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {
    
    public Material HitMateral;
    private Renderer rend;
    //private Color HitColor = Color.red;
    public Material thisMaterial;
    private Material OwnMaterial;
    public Color thisColor;
    public float currentHP;
    private float MaxHP;
    private bool defeated = false;
    private bool isHit = false;
    private float HitTime = 0;
    private int Type = 1;
    private float HitCoolDown = 0;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        if (MaxHP <= 0) {
            if (Type == 1)
            {
                MaxHP = 100;
                HitCoolDown = 1.0f;
            }
        }
        currentHP = MaxHP;
        thisMaterial = rend.material;
        OwnMaterial = thisMaterial;
        //Debug.Log(thisMaterial.name);
    }
	
	// Update is called once per frame
	void Update () {
	    if (defeated)
        {
            shrinkobject();
        }
        if (isHit && Time.time > HitTime)
        {
            isHit = false;
            //if (thisMaterial) { rend.material.CopyPropertiesFromMaterial(thisMaterial); }
        }
	}

    public void HitTarget(float damage, Vector3 force)
    {
        currentHP = currentHP - damage;
        if (currentHP > 0)
        {
            Debug.Log(gameObject.name + " damaged, HP:  " + currentHP + "  D:    " + damage + "  MHP:  " + MaxHP);
        }
        else
        {
            if (currentHP < 0) { currentHP = 0; }
            Debug.Log(gameObject.name + " defeated, HP:  " + currentHP + "  D:     " + damage + "  MHP:  " + MaxHP);
            defeated = true;
            if (HitMateral) { rend.material.CopyPropertiesFromMaterial(HitMateral); }
        }
        isHit = true;
        HitTime = Time.time + HitCoolDown;
        //if (HitMateral) { rend.material.CopyPropertiesFromMaterial(HitMateral); }
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
