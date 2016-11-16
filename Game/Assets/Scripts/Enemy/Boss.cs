using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour {
    [Header("Boss Requirements")]
    public EnemyHP EnemyHPScript;
    public GameObject BossGUI;
    public Image Healthbar;
    public Image FadeOutScreen;
    [Header("Boss Objects")]
    public GameObject ShieldObject;

    private int Fase = 1;
    private int AttackSelector = 0;
    private float NextAttackDelay = 0;
    private float NextAttackTime = 0;
    private float AttackTimeWait = 0;
    private float AttackTime = 0;
    private bool CanAttack = true;
    [Header("Boss Stats")]
    public float BossHP = 1000;
    private float OldBossHP = 0;
    private float BossCurrentHP = 0;
    private float Switch1HP = 800;
    private float Switch2HP = 350;

    private bool Active = true;
    public bool Beaten = false;

    private bool Invincible = false;

    public float TimeSlowValue = 1;
    private float FadeOutValue = 0;
    public bool FadeOut = false;

    private bool GoToFinishScreen = false;

    // Use this for initialization
    void Start () {
        BossCurrentHP = BossHP;
        OldBossHP = BossHP;
        if (EnemyHPScript && BossGUI && Healthbar)
        {
            EnemyHPScript.SetHP(BossHP);
            EnemyHPScript.SetIsBoss(true);
            BossGUI.SetActive(true);
            Healthbar.fillAmount = 1;
        } else
        {
            Active = false;
            Debug.LogWarning("BossFight couldn't start, assign objects!");
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Active && !Beaten)
        {
            CheckHealth();
            Attack();
        }
        if (Active && Beaten)
        {
            Defeaten();
        }

    }

    void CheckHealth()
    {
        if (OldBossHP != BossHP) { OldBossHP = BossHP; BossCurrentHP = BossHP; }
        BossCurrentHP = EnemyHPScript.GetHP();
        switch (Fase)
        {
            case 1:
                if (BossCurrentHP < Switch1HP)
                {
                    BossCurrentHP = Switch1HP;
                    Fase++;
                    Debug.Log("[BOSS] Fase 1 beaten.");
                }
                break;
            case 2:
                if (BossCurrentHP < Switch2HP)
                {
                    BossCurrentHP = Switch2HP;
                    Fase++;
                    Debug.Log("[BOSS] Fase 2 beaten.");
                }
                break;
            case 3:
                if (BossCurrentHP < 0)
                {
                    Beaten = true;
                    BossCurrentHP = 0;
                    Fase++;
                    BossGUI.SetActive(false);
                    Debug.Log("[BOSS] Fase 3 beaten.");
                }
                break;
        }

        Healthbar.fillAmount = BossCurrentHP / BossHP;
    }

    void Defeaten()
    {
        // Animation

        // Slowdown
        if (TimeSlowValue > 0.25)
        {
            Time.fixedDeltaTime = TimeSlowValue;
            Time.timeScale = TimeSlowValue;
            TimeSlowValue -= 0.01f;
            if (TimeSlowValue < 0.25) { TimeSlowValue = 0.25f; }
        }
        if (FadeOut)
        {
            if (FadeOutValue < 1)
            {
                FadeOutValue += ((0.01f * TimeSlowValue) * TimeSlowValue) * Time.deltaTime;
                Color ne = FadeOutScreen.color;
                ne.a = FadeOutValue;
                FadeOutScreen.color = ne;
            }
        }
        if (GoToFinishScreen)
        {
            // sceneaanger.changescne 
        }
    }

    void Attack()
    {
        if (CanAttack)
        {
            Debug.Log("[BOSS] Rolling for new attack");
            CanAttack = false;
            switch (Fase)
            {
                case 1:
                    AttackSelector = Random.Range(1, 4);
                    break;
                case 2:
                    AttackSelector = Random.Range(1, 6);
                    break;
                case 3:
                    AttackSelector = Random.Range(1, 8);
                    break;
            }

            switch (AttackSelector)
            {
                case 1:
                    AttackTime = 3.0f;
                    NextAttackTime = 2.5f;
                    StartCoroutine(MagicSpell(AttackTime));
                    break;
                case 2:
                    AttackTime = 1.0f;
                    NextAttackTime = 2.5f;
                    break;
                case 3:
                    AttackTime = 2.5f;
                    NextAttackTime = 1;

                    break;
                case 4:
                    AttackTime = 5;
                    NextAttackTime = 5;
                    break;
                default:
                    Debug.LogError("[BOSS] Got an invalid AttackSelector number:" + AttackSelector);
                    CanAttack = true;
                    break;
            }
            AttackTimeWait = Time.time + AttackTime;
            NextAttackDelay = NextAttackTime + AttackTimeWait;
            //Debug.Log("Got Attack: " + AttackSelector + " with AT: " + AttackTime + "ATW: " + AttackTimeWait + " NAT: " + NextAttackTime + " NAD: " + NextAttackDelay);
        }
        else
        {
            if (Time.time > NextAttackDelay)
            {
                CanAttack = true;
            }
        }
    }

    IEnumerator MagicSpell(float attacktime)
    {
        Debug.Log("[BOSSATTACK] Cast MagicSpell with attacktime " + attacktime);
        yield return new WaitForSeconds(attacktime);
    }
}
