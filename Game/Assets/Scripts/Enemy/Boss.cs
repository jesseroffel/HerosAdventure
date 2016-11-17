using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour {

    [Header("Boss Requirements")]
    public EnemyHP EnemyHPScript;
    public GameObject BossGUI;
    public Image Healthbar;
    public Image FadeOutScreen;
    public GameObject Player;

    [Header("Boss Objects")]
    public GameObject ShieldObject;
    private Transform HomeLocation;
    public Transform BossMissle;
    public Transform RangeSkeleton;
    public Transform MeleeSkeleton;
    public Transform Slime;

    private int Fase = 1;
    private int AttackSelector = 0;
    private int PreviousAttack = 0;
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
    private int SpawnedEnemiesActive = 0;

    public float TimeSlowValue = 1;
    private float FadeOutValue = 0;
    public bool FadeOut = false;

    private bool GoToFinishScreen = false;

    private int CurrentRadius = 0;
    private int CurrentIndex = 0;
    private float SpawnRadius = 2.0f;
    private int RadiusDistance = 0;
    private int SpawnAmount = 0;

    // Use this for initialization
    void Start () {
        BossCurrentHP = BossHP;
        OldBossHP = BossHP;
        if (EnemyHPScript && BossGUI && Healthbar && Player)
        {
            EnemyHPScript.SetHP(BossHP);
            EnemyHPScript.SetIsBoss(true);
            BossGUI.SetActive(true);
            Healthbar.fillAmount = 1;
            HomeLocation = transform;
            LookAt();
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

    void LookAt()
    {
        transform.LookAt(Player.transform);
        //float rot = transform.rotation.eulerAngles.x;
        //if (rot > 0) { transform.Rotate(-rot, 0, 0, Space.World); } else { transform.Rotate(rot, 0, 0, Space.World); }
        //Vector3 relativePos = Player.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = rotation;

        //Vector3 lookAtPosition = Player.transform.position;
        //lookAtPosition.y = transform.position.y;
        //lookAtPosition.z = transform.position.z;
        //transform.LookAt(lookAtPosition);
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
                    do { AttackSelector = Random.Range(3, 5); } while (AttackSelector == PreviousAttack);
                    break;
                case 2:
                    AttackSelector = Random.Range(1, 6);
                    break;
                case 3:
                    AttackSelector = Random.Range(1, 8);
                    break;
            }
            PreviousAttack = AttackSelector;
            switch (AttackSelector)
            {
                case 1:
                    AttackTime = 3.0f;
                    NextAttackTime = 2.5f;
                    StartCoroutine(MagicSpell(AttackTime));
                    break;
                case 2:
                    AttackTime = 2.0f;
                    NextAttackTime = 2.5f;
                    StartCoroutine(QuickShield(AttackTime));
                    break;
                case 3:
                    AttackTime = 2.5f;
                    NextAttackTime = 1;
                    StartCoroutine(CircleMissleAttack(AttackTime));
                    break;
                case 4:
                    AttackTime = 5;
                    NextAttackTime = 5;
                    StartCoroutine(SpawnRangeSkeletons(AttackTime));
                    break;
                case 5:
                    break;
                case 6:
                    AttackTime = 5;
                    NextAttackTime = 5;
                    StartCoroutine(MultipleCircleMissle(AttackTime));
                    break;
                case 7:
                    break;
                case 8:
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

    Vector3 CalcCircle(Vector3 center, float radius, int angle)
    {
        float ang = angle;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    int CalcDistance(int input)
    {
        int am = input;
        int calc = 360 / am;
        return calc;
    }

    IEnumerator MagicSpell(float attacktime)
    {
        Debug.Log("[BOSSATTACK] Cast MagicSpell with attacktime " + attacktime);
        for (int i = 0; i < 3; i++)
        {
            for (int o = 0; o < 10; o++)
            {
                LookAt();
                yield return new WaitForSeconds(0.1f);
            }
            Transform Ball = (Transform)Instantiate(BossMissle, transform.position + (transform.forward), transform.rotation);
            Ball.transform.position += new Vector3(0, 0.4f, 0);
            Ball.GetComponent<HitRegistrator>().SetSettings(4, 5, 25, Vector3.zero, true);
        }
        Debug.Log("Finished");
        yield return new WaitForSeconds(attacktime);
    }

    IEnumerator QuickShield(float attacktime)
    {
        if (ShieldObject.activeSelf == false) {
            ShieldObject.SetActive(true);
            EnemyHPScript.SetIsInvincible(true);
        }
        LookAt();
        Debug.Log("[BOSSATTACK] Cast QuickShield with attacktime " + attacktime);
        yield return new WaitForSeconds(attacktime);
        ShieldObject.SetActive(false);
        EnemyHPScript.SetIsInvincible(false);
        yield return new WaitForSeconds(0);
    }

    IEnumerator SpawnRangeSkeletons(float attacktime)
    {
        SpawnAmount = 6;
        CurrentIndex = 1;
        CurrentRadius = 0;
        SpawnRadius = 8;
        RadiusDistance = CalcDistance(SpawnAmount);
        for (int i = 0; i < SpawnAmount; i++)
        {
            Vector3 Calcpos = CalcCircle(transform.position, SpawnRadius, CurrentRadius);

            Transform Skeleton = (Transform)Instantiate(RangeSkeleton, Calcpos, Quaternion.identity);
            Skeleton.Rotate(0, CurrentRadius, 0, Space.World);

            if (CurrentRadius != 360)
            {
                CurrentRadius = CurrentIndex * RadiusDistance;
                CurrentIndex++;
            }
        }

        Debug.Log("[BOSSATTACK] Cast SpawnRangeSkeletons with attacktime " + attacktime);
        yield return new WaitForSeconds(attacktime);
    }

    IEnumerator CircleMissleAttack(float attacktime)
    {

        SpawnAmount = 6;
        CurrentIndex = 1;
        CurrentRadius = 0;
        SpawnRadius = 2;
        RadiusDistance = CalcDistance(SpawnAmount);
        for (int i = 0; i < SpawnAmount;i++)
        {
            Vector3 Calcpos = CalcCircle(transform.position, SpawnRadius, CurrentRadius);

            Transform Ball = (Transform)Instantiate(BossMissle, Calcpos, Quaternion.identity);
            Ball.Rotate(0, CurrentRadius, 0, Space.World);
            Ball.GetComponent<HitRegistrator>().SetSettings(4, 5, 25, Vector3.zero, true);

            if (CurrentRadius != 360)
            {
                CurrentRadius = CurrentIndex * RadiusDistance;
                CurrentIndex++;
            }
        }
        Debug.Log("[BOSSATTACK] Cast CircleMissleAttack with attacktime " + attacktime + " and SpawnAmount: " + SpawnAmount);
        yield return new WaitForSeconds(attacktime);
    }

    IEnumerator MultipleCircleMissle(float attacktime)
    {
        int rings = 5;// Random.Range(2, 6);
        int ringsdis = CalcDistance(rings);
        int ringindex = 1;
        SpawnRadius = 2;
        for (int s = 0; s < rings; s++)
        {
            SpawnAmount = 6;
            CurrentIndex = 1;

            if (ringindex != 1)
            {
                int curin = ringindex - 1;
                int added = curin * ringsdis;
                CurrentRadius += added;
                if (CurrentRadius > 360) { CurrentRadius = CurrentRadius - 360; }
            } else
            {
                CurrentRadius = 0;
            }

            RadiusDistance = CalcDistance(SpawnAmount);
            for (int i = 0; i < SpawnAmount; i++)
            {
                Vector3 Calcpos = CalcCircle(transform.position, SpawnRadius, CurrentRadius);

                Transform Ball = (Transform)Instantiate(BossMissle, Calcpos, Quaternion.identity);
                Ball.Rotate(0, CurrentRadius, 0, Space.World);
                Ball.GetComponent<HitRegistrator>().SetSettings(4, 5, 25, Vector3.zero, true);

                if (ringindex < 2)
                {
                    if (CurrentRadius != 360)
                    {
                        CurrentRadius = CurrentIndex * RadiusDistance;
                        CurrentIndex++;
                    }
                }
            }
            transform.Rotate(0, ringsdis, 0, Space.World);
            ringindex++;
            yield return new WaitForSeconds(0.33f);
        }
        
        Debug.Log("[BOSSATTACK] Cast CircleMissleAttack with attacktime " + attacktime + " and SpawnAmount: " + SpawnAmount);
        yield return new WaitForSeconds(attacktime);

    }
}
