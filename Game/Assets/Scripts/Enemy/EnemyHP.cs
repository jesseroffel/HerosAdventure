using UnityEngine;
using System.Collections.Generic;

public class EnemyHP : MonoBehaviour {

    public Material[] Materials;
    // 0 = own
    // 1 = hit
    // 2 = slowness
    // 3 = poison
    // 4 = fire
    private int currentmat = 0;
    private Renderer rend;

    public float currentHP;
    private int EnemyID = 1;
    private List<int> EffectsApplied = new List<int>();
    private List<float> EffectTime = new List<float>();
    private float MaxHP;
    private bool isHit = false;
    public bool defeated = false;
    private float HitTime = 0;
    private float HitCoolDown = 0.25f;
    private bool Sendkill = false;
    private bool HitKnockback = false;
    private Vector3 KnockbackForce;

    //Effects
    EnemyAI EAI;

    private float oldspeed = 0;
    private float poisondamage = 0;
    private float poisonwait = 0;
    private bool poison = false;

    enum EffectNames { Test, MagicMissle, AeOSlowdown, AoEPoison}

    // Use this for initialization
    void Start () {
        //SetEnemyID();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = Materials[0];
        if (MaxHP <= 0) {
            if (EnemyID == 1)
            {
                MaxHP = 100;
            }
        }
        currentHP = MaxHP;

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
        
        CheckEffect();
        CheckDebufs();

        if (isHit && Time.time > HitTime)
        {
            rend.sharedMaterial = Materials[currentmat];
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

    void CheckDebufs()
    {
        if (poison)
        {
            if (poisonwait < Time.time)
            {
                HitTarget(poisondamage, Vector3.zero);
                poisonwait = Time.time + 1;
            }
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
            Debug.Log("[ENEMY] " + gameObject.name + " damaged. [HP] " + currentHP + " [DAMAGE] " + damage);
        }
        else
        {
            if (currentHP < 0) { currentHP = 0; }
            Debug.Log("[ENEMY] " + gameObject.name + " damaged. [HP] " + currentHP + " [DAMAGE] " + damage + " [MAXHP] " + MaxHP);
            defeated = true;
            // Notify Questlist of kill for quests
            QuestList.QuestListObject.RegisterKillID(EnemyID);

            rend.sharedMaterial = Materials[1];
        }
        isHit = true;
        HitTime = Time.time + HitCoolDown;
        rend.sharedMaterial = Materials[1];
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
        ApplyActiveEffects(duration);
    }

    void ApplyActiveEffects(float duration)
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
                        DebufPoison();
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
        currentmat = 2;
        if (oldspeed == 0) { oldspeed = EAI.speed; }
        EAI.SetSpeed(EAI.speed / 2);
        rend.sharedMaterial = Materials[currentmat];
        //rend.sharedMaterial.CopyPropertiesFromMaterial(Materials[0]);
    }

    void DebufBurn()
    {
        currentmat = 4;
        rend.sharedMaterial = Materials[currentmat];
    }

    public void SetMaterial(int index)
    {
        rend.sharedMaterial = Materials[index];

    }

    void DebufPoison()
    {
        currentmat = 3;
        if (poison == false) { poison = true; }
        poisondamage += 10;
        rend.sharedMaterial = Materials[currentmat];
        poisonwait = Time.time + 1;
        //rend.sharedMaterial.CopyPropertiesFromMaterial(Materials[0]);
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
        if (!check) { RemoveDebuff(Idex); rend.sharedMaterial = Materials[0]; }
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
                poison = false;
                break;
        }
    }

}
