using UnityEngine;
using System.Collections.Generic;

public class EnemyHP : MonoBehaviour {
    
    public Material HitMateral;
    private Renderer rend;
    //private Color HitColor = Color.red;
    //public Material thisMaterial;
    private Material OwnMaterial;
    //public Color thisColor;

    public float currentHP;
    private int EnemyID = 1;
    private List<int> EffectsApplied = new List<int>();
    private List<float> EffectTime = new List<float>();
    private float MaxHP;
    private bool isHit = false;
    public bool defeated = false;
    private float HitTime = 0;
    private float HitCoolDown = 0;
    private bool Sendkill = false;
    private bool HitKnockback = false;
    private Vector3 KnockbackForce;

    //Effects
    EnemyAI EAI;
    float oldspeed = 0;

    enum EffectNames { Test, MagicMissle, AeOSlowdown, AoEPoison}

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

        if (EAI == null)
        {
            EAI = gameObject.GetComponent<EnemyAI>();
        }
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
        CheckEffect();

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

    public void AddEffect(int effectID, float duration)
    {
        EffectsApplied.Add(effectID);
        EffectTime.Add(Time.time + duration);
        ApplyActiveEffects();
    }

    void ApplyActiveEffects()
    {
        int Amount = EffectsApplied.Count;
        if (Amount > 0)
        {
            for (int i = 0; i <Amount; i++)
            {
                int EI = EffectsApplied[i];
                switch (EI)
                {
                    case 2:
                        DebufSpeed();
                        break;
                    case 3:
                        break;

                }
            }
        }
    }

    void CheckEffect()
    {
        int Amount = EffectTime.Count;
        if (Amount > 0)
        {
            for (int i = 0; i < Amount; i++)
            {
                if (EffectTime[i] < Time.time)
                {
                    int type = EffectsApplied[i];
                    bool check = RemoveEffect(type);
                    if (check)
                    {
                        Amount = 0;
                        Debug.Log("[MAGIC] Effect " + i + " has worn out");
                    }
                    else
                    {
                        Debug.LogWarning("[MAGIC] Effect could not be removed!?");
                    }
                }
            }
        }
    }

    void DebufSpeed()
    {
        if (oldspeed == 0) { oldspeed = EAI.speed; }
        EAI.SetSpeed(EAI.speed / 2);
    }

    bool RemoveEffect(int Idex)
    {
        int index = 0;
        bool endcheck = false;
        for (int i = 0; i < EffectsApplied.Count; i++) {
            if (EffectsApplied[i] == Idex)
            {
                index = i;
                EffectTime.RemoveAt(index);
                EffectsApplied.RemoveAt(index);
                endcheck = true;
                break;
            }
        }
        bool check = false;
        for (int i = 0; i < EffectsApplied.Count; i++)
        {
            if (EffectsApplied[i] == Idex) { check = true; }
        }
        if (!check) { RemoveDebuff(Idex); }
        if (endcheck) { return true; } else { return false; }
    }

    void RemoveDebuff(int debufid)
    {
        switch (debufid)
        {
            case 2:
                EAI.SetSpeed(oldspeed);
                oldspeed = 0;
                break;
            case 3:
                break;
        }
    }

}
