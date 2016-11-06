using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {
    
    public Material HitMateral;
    private Renderer rend;
    //private Color HitColor = Color.red;
    //public Material thisMaterial;
    private Material OwnMaterial;
    //public Color thisColor;

    private int EnemyID = 1;
    public float currentHP;
    private float MaxHP;
    private bool isHit = false;
    private bool defeated = false;
    private float HitTime = 0;
    private float HitCoolDown = 0;
    private bool Sendkill = false;
    private bool HitKnockback = false;
    private Vector3 KnockbackForce;

    // Use this for initialization
    void Start () {
        //SetEnemyID();
        rend = GetComponent<Renderer>();
        if (MaxHP <= 0) {
            if (EnemyID == 1)
            {
                MaxHP = 100;
                HitCoolDown = 1.0f;
            }
        }
        currentHP = MaxHP;
        //thisMaterial = rend.material;
        //OwnMaterial = thisMaterial;
        //Debug.Log(thisMaterial.name);
    }
	
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

    void FixedUpdate()
    {
        if (HitKnockback)
        {
            HitKnockback = false;
            transform.GetComponent<Rigidbody>().AddForce(KnockbackForce, ForceMode.Impulse);
        }
    }

    void SetEnemyID()
    {
        if (EnemyID == 0)
        {
            // GET INFO FROM LIST
            // EnemyID =
            // MaxHP =
            // currenthp = MaxHP;
            // HitCoolDown =
        }
    }

    public void HitTarget(float damage, Vector3 force)
    {
        currentHP = currentHP - damage;
        if (currentHP > 0)
        {
            Debug.Log("[ENEMY] " + gameObject.name + " damaged, HP:  " + currentHP + "  D:    " + damage + "  MHP:  " + MaxHP);
        }
        else
        {
            if (currentHP < 0) { currentHP = 0; }
            Debug.Log("[ENEMY] " + gameObject.name + " defeated, HP:  " + currentHP + "  D:     " + damage + "  MHP:  " + MaxHP);
            defeated = true;
            // Notify Questlist of kill for quests
            QuestList.QuestListObject.RegisterKillID(EnemyID);

            if (HitMateral) { rend.material.CopyPropertiesFromMaterial(HitMateral); }
        }
        isHit = true;
        HitTime = Time.time + HitCoolDown;
        //if (HitMateral) { rend.material.CopyPropertiesFromMaterial(HitMateral); }
        KnockbackForce = force;
        HitKnockback = true;
        
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
