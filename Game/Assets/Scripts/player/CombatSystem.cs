using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CombatSystem : MonoBehaviour {
    [Header("Player Objects")]
    public FirstPersonControler FirstPersonControlerScript;
    public Animator PlayerAnimator;
    public Player_Health PlayerStats;
    public AudioClip[] CombatFxs;
    private AudioSource AudioSource;
    [Header("Combat Prefabs")]
    public Transform HitRegBlock;
    public GameObject ArrowPrefab;
    public GameObject MagicMisslePrefab;
    public GameObject AoEPrefab;

    [Header("Player GameObjects")]
    public Transform ArrowSpawn;
    public Transform MagicMissleSpawn;
    public Transform MagicAreaOfEffectSpawn;
    public GameObject SwordModel;
    public GameObject StaffModel;
    public GameObject BowModel;
    //public Transform HandPosition;
    //public Transform BackPosition01;
    //public Transform BackPosition02;

    [Header("Attack Settings")]
    //Melee
    public int AttackOrder = 1;
    private float Attack01Swing = 0.60f;
    private float Attack02Swing = 0.60f;
    private float Attack03Swing = 0.60f;
    private float AttackBuildup = 0.5f;
    private float AttackWait = 0.55f;
    //private float UIWaitTime = 0;
    private float NextMeleeAttack = 0;
    private bool WaitForNextInput = false;

    public float propulsionForce = 100.0f;
    private float NextAttack = 0.0f;
    private float WaitForSpawn = 0.3f;
    private float AttackTime = 0.0f;

    private int CombatState = 1;
    private bool PrepareAttack = false;
    private bool Attacking = false;

    private float MaxCharge = 0;
    private float CurrentCharge = 0;
    private float ApplyCharge = 0;

    //Range
    private float WaitForNextArrow = 0;

    //Magic
    private Spell currentspell;
    public int MagicSpell = 1;
    private int maxspells = 0;
    private float spelldowncool = 0;
    private float MagicSwitchDelay = 0;
    private float DelayAdd = 1.5f;
    private bool PreAttackSpawned = false;
    private bool SpawningMissle = false;
    private bool SpawningAoE = false;

    //Switching
    public float SwitchSpeed = 1.0f;
    private float SwitchDisable = 0;
    private bool FinishSwitch = false;
    private bool SwitchChosen = false;
    private int SwitchChosenOption = 0;
    private int[] StyleOrder = { 2, 1, 3 };

    // UI
    [Header("Combat Switch UI")]
    public GameObject SwitchCombatPanel;
    private CombatSwitchUI CombatSwitchUIScript;

    [Header("Magic Switch UI")]
    public GameObject SwitchMagicSpellPanel;
    private ChangeSpellUI ChangeSpellUI;

    [Header("Melee Attack UI")]
    public Transform SwordsPanel;
    private Sprite UnfilledSwordSprite;
    private Sprite FilledSwordSprite;

    [Header("Bow Attack UI")]
    public GameObject BowChargePanel;
    public Image ArrowChargeMeter;
    private float BowStrengh = 0;

    [Header("Magic Attack UI")]
    public GameObject MagicChargePanel;
    public Image MagicChargeMeter;
    private float MagicCharged = 0;


    private bool HoldingDown = false;
    private bool WindowOpen = false;
    private bool SetWalkSpeed = false;

    private float AttackPower = 35.0f;
    private int SoundSelector = 0;
    private float AudioVolume = 0.5f;

    private Quaternion SwordRot = new Quaternion(50, 0, 0, 0);

    enum CombatStyle { NoCombat = 0, Melee = 1, Range = 2, Magic = 3 };
    enum MagicStyle { None = 0, Missle = 1, AoE = 2}

    // Use this for initialization
    void Start() {
        //myTransform = gameObject.position;
        if (PlayerAnimator == null) { Debug.LogError("Animator 'PlayerAnimator' is null, set reference"); }
        if (HitRegBlock == null) { Debug.LogError("Transform 'HitRegBlock' is null, set reference"); }
        if (FirstPersonControlerScript == null) { Debug.LogError("Player_move 'playermovescript' is null, set reference"); }
        if (ArrowChargeMeter == null) { Debug.LogError("Image 'ArrowChargeMeter' is null, set reference"); }
        if (BowChargePanel == null) { Debug.LogError("GameObject 'BowChargePanel' is null, set reference"); }

        if (SwordsPanel == null) { Debug.LogError("Transform 'SwordsPanel' is null, set reference"); }

        if (ChangeSpellUI == null) { ChangeSpellUI = SwitchMagicSpellPanel.GetComponent<ChangeSpellUI>(); }

        if (UnfilledSwordSprite == null) { UnfilledSwordSprite = Resources.Load<Sprite>("Sprites/UI/unfilledsword"); }
        if (FilledSwordSprite == null) { FilledSwordSprite = Resources.Load<Sprite>("Sprites/UI/filledsword"); }
        if (AudioSource == null) { AudioSource = GetComponent<AudioSource>(); }
        maxspells = Spellbook.SpellbookObject.AmountSpells;
        currentspell = Spellbook.SpellbookObject.GetSpellByID(MagicSpell);
        ChangeSpellUI.SetSpellName(currentspell.SpellName);
        ChangeSpellUI.SetSpellImage(currentspell.Sprite);
        ChangeSpellUI.SetCost(currentspell.ManaCost);
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        CheckCurrentAttack();
        CheckSpellCooldown();
        CheckNewAttack();

        if (Attacking)
        {
            if (Time.time > NextMeleeAttack)
            {
            }

        }
    }

    void CheckInput()
    {
        if (FirstPersonControlerScript.CanMove)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                SetAttackAnimation();
                if (AttackOrder != 1) { WaitForNextInput = false; }
            }
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                HoldingDown = false;
            }

            if (CrossPlatformInputManager.GetButtonDown("SwitchSpell") && CombatState == (int)CombatStyle.Magic && Time.time > MagicSwitchDelay)
            {
                SwitchSpell();
            }

            if (CrossPlatformInputManager.GetButton("SwitchCombat") && Time.time > SwitchDisable)
            {
                SwitchCombatStyle();
            }

            if (CrossPlatformInputManager.GetButtonUp("SwitchCombat"))
            {
                WindowOpen = false;
                SwitchCombatPanel.SetActive(false);
            }
        }
    }

    void CheckCurrentAttack()
    {
        if (Attacking)
        {
            if (Time.time > NextAttack)
            {
                Attacking = false;
                AttackOrder = 1;
                AttackPower = 35f;
                if (FirstPersonControlerScript) { FirstPersonControlerScript.CanMove = true; }
                if (CombatState == 1)
                {
                    if (SwordsPanel)
                    {
                        SwordsPanel.GetChild(0).GetComponent<Image>().sprite = UnfilledSwordSprite;
                        SwordsPanel.GetChild(1).GetComponent<Image>().sprite = UnfilledSwordSprite;
                        SwordsPanel.GetChild(2).GetComponent<Image>().sprite = UnfilledSwordSprite;
                    }
                }
                FirstPersonControlerScript.SetSpeedNormal();
            }
        }
    }

    void CheckSpellCooldown()
    {
        if (spelldowncool != 1)
        {
            spelldowncool += DelayAdd;
            if (spelldowncool > 1) { spelldowncool = 1; }
            ChangeSpellUI.SetFill(spelldowncool);
        }
    }

    void CheckNewAttack()
    {
        if (PrepareAttack && !WaitForNextInput)
        {
            if (Time.time > AttackTime)
            {
                Attacking = true;
                switch (CombatState)
                {
                    case (int)CombatStyle.Melee:
                        if (Time.time > NextMeleeAttack)
                        {
                            float walkspeed = 0;
                            if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
                            {
                                SoundSelector = Random.Range(1, 4);
                                AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
                            }
                            switch (AttackOrder)
                            {
                                case 1:
                                    NextMeleeAttack = Time.time + Attack01Swing;
                                    NextAttack = NextMeleeAttack + AttackWait;
                                    if (SwordsPanel) { SwordsPanel.GetChild(0).GetComponent<Image>().sprite = FilledSwordSprite; }

                                    walkspeed = FirstPersonControlerScript.GetNormalWalkspeed();
                                    FirstPersonControlerScript.SetWalkSpeed(walkspeed * 0.80f);

                                    Transform HitDec1 = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                                    HitDec1.transform.parent = transform;
                                    HitDec1.transform.position += new Vector3(0, 0.4f, 0);
                                    HitDec1.GetComponent<HitRegistrator>().SetSettings(1, Attack01Swing, AttackPower, transform.forward * propulsionForce);

                                    AttackOrder++;
                                    WaitForNextInput = true;
                                    break;
                                case 2:
                                    PlayerAnimator.SetTrigger("AttackMelee01Trigger");
                                    AttackPower = 40f;
                                    NextMeleeAttack = Time.time + Attack02Swing;
                                    NextAttack = NextMeleeAttack + AttackWait;
                                    if (SwordsPanel) { SwordsPanel.GetChild(1).GetComponent<Image>().sprite = FilledSwordSprite; }

                                    walkspeed = FirstPersonControlerScript.GetNormalWalkspeed();
                                    FirstPersonControlerScript.SetWalkSpeed(walkspeed * 0.6f);

                                    Transform HitDec2 = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                                    HitDec2.transform.parent = transform;
                                    HitDec2.transform.position += new Vector3(0, 0.4f, 0);
                                    HitDec2.GetComponent<HitRegistrator>().SetSettings(1, Attack02Swing, AttackPower, transform.forward * propulsionForce);
                                    AttackPower = 45f;
                                    AttackOrder++;
                                    WaitForNextInput = true;                     
                                    break;
                                case 3:
                                    PlayerAnimator.SetTrigger("AttackMelee01Trigger");
                                    NextMeleeAttack = Time.time + Attack03Swing;
                                    NextAttack = NextMeleeAttack + AttackWait;
                                    if (SwordsPanel) { SwordsPanel.GetChild(2).GetComponent<Image>().sprite = FilledSwordSprite; }

                                    walkspeed = FirstPersonControlerScript.GetNormalWalkspeed();
                                    FirstPersonControlerScript.SetWalkSpeed(walkspeed * 0.4f);

                                    Transform HitDec3 = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                                    HitDec3.transform.parent = transform;
                                    HitDec3.transform.position += new Vector3(0, 0.4f, 0);
                                    HitDec3.GetComponent<HitRegistrator>().SetSettings(1, Attack02Swing, AttackPower, transform.forward * propulsionForce);

                                    PrepareAttack = false;
                                    WaitForNextInput = false;
                                    break;
                            }
                        }
                        //Debug.Log("Time: " + Time.time + " NextMeleeAttack: " + NextMeleeAttack + " NextAttack: " + NextAttack);
                        break;
                    case (int)CombatStyle.Range:
                        if (Time.time > WaitForNextArrow)
                        {
                            if (HoldingDown)
                            {
                                if (SetWalkSpeed)
                                {
                                    SetWalkSpeed = false;
                                    float NewWalkSpeed = FirstPersonControlerScript.GetNormalWalkspeed();
                                    FirstPersonControlerScript.SetWalkSpeed(NewWalkSpeed * 0.33f);
                                }
                                if (BowChargePanel)
                                {
                                    if (BowChargePanel.activeSelf == false) { BowChargePanel.SetActive(true); }
                                }
                                if (BowStrengh < 1)
                                {
                                    BowStrengh += 0.01f;
                                    if (BowStrengh > 1) { BowStrengh = 1; }

                                }
                                if (ArrowChargeMeter) { ArrowChargeMeter.fillAmount = BowStrengh; }
                            }
                            else
                            {
                                if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
                                {
                                    SoundSelector = Random.Range(4, 6);
                                    AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
                                }
                                WaitForNextArrow = Time.time + 0.75f;
                                SetWalkSpeed = true;
                                PrepareAttack = false;
                                GameObject Projectile = Instantiate(ArrowPrefab);
                                Projectile.transform.position = ArrowSpawn.position;
                                Projectile.transform.rotation = ArrowSpawn.rotation;

                                Rigidbody rb = Projectile.GetComponent<Rigidbody>();
                                if (BowStrengh < 0.25f) { rb.velocity = (ArrowSpawn.forward * 350) * 0.25f; } else { rb.velocity = (ArrowSpawn.forward * 200) * BowStrengh; }
                                

                                Projectile.GetComponent<HitRegistrator>().SetSettings(2, 10, 10, transform.forward * propulsionForce);
                                if (BowChargePanel) { BowChargePanel.SetActive(false); }
                                BowStrengh = 0;

                                FirstPersonControlerScript.SetSpeedNormal();
                            }
                        }
                        break;
                    case (int)CombatStyle.Magic:
                        if (spelldowncool == 1.0f)
                        {
                            int type = currentspell.SpellType;

                            if (PlayerStats.Mana >= currentspell.ManaCost)
                            {
                                if (HoldingDown)
                                {
                                    if (SetWalkSpeed) {
                                        SetWalkSpeed = false;
                                        float NewWalkSpeed = FirstPersonControlerScript.GetNormalWalkspeed();
                                        FirstPersonControlerScript.SetWalkSpeed(NewWalkSpeed * 0.45f);
                                    }
                                    if (MaxCharge == 0) { MaxCharge = currentspell.CastTime; }
                                    if (MagicChargePanel) {
                                        if (MagicChargePanel.activeSelf == false) {
                                            MagicChargePanel.SetActive(true);
                                            if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
                                            {
                                                SoundSelector = Random.Range(6, 10);
                                                AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
                                            }
                                        }
                                    }
                                    if (MagicCharged < 1)
                                    {
                                        MagicCharged = CurrentCharge / MaxCharge;
                                        if (MagicCharged > 1) { MagicCharged = 1; }
                                    }
                                    if (MagicChargeMeter) { MagicChargeMeter.fillAmount = MagicCharged; }
                                    if (CurrentCharge < MaxCharge)
                                    {
                                        if (!PreAttackSpawned)
                                        {
                                            switch (type)
                                            {
                                                case 1:
                                                    GameObject Missle = Instantiate(MagicMisslePrefab);
                                                    Missle.transform.parent = MagicMissleSpawn;
                                                    Missle.transform.position = MagicMissleSpawn.position;
                                                    Missle.transform.rotation = MagicMissleSpawn.rotation;
                                                    Missle.GetComponent<HitRegistrator>().enabled = false;
                                                    SpawningMissle = true;
                                                    PreAttackSpawned = true;
                                                    break;
                                                case 2:
                                                    GameObject AoE = Instantiate(AoEPrefab);
                                                    AoE.transform.parent = MagicAreaOfEffectSpawn;
                                                    AoE.transform.position = MagicAreaOfEffectSpawn.position;
                                                    AoE.transform.rotation = MagicAreaOfEffectSpawn.rotation;
                                                    AoE.GetComponent<HitRegistrator>().enabled = false;
                                                    SpawningAoE = true;
                                                    PreAttackSpawned = true;
                                                    break;
                                            }
                                        }

                                        if (SpawningMissle)
                                        {
                                            if (MagicMissleSpawn.transform.childCount == 1)
                                            {
                                                Transform Movement = MagicMissleSpawn.transform.GetChild(0);
                                                Movement.localScale += new Vector3(CurrentCharge, CurrentCharge * 0.15f, CurrentCharge);
                                                ApplyCharge = Movement.lossyScale.x;
                                                Debug.Log(Movement.localScale);
                                            }
                                        }

                                        if (SpawningAoE)
                                        {
                                            if (MagicAreaOfEffectSpawn.transform.childCount == 1)
                                            {
                                                Transform Movement = MagicAreaOfEffectSpawn.transform.GetChild(0);
                                                Movement.localScale += new Vector3(CurrentCharge, 0, CurrentCharge);
                                                ApplyCharge = Movement.lossyScale.x;
                                            }
                                        }
                                        CurrentCharge += 0.01f;

                                    }
                                } else
                                {
                                    
                                    if (SpawningAoE) { Destroy(MagicAreaOfEffectSpawn.transform.GetChild(0).gameObject); }
                                    if (SpawningMissle) { Destroy(MagicMissleSpawn.transform.GetChild(0).gameObject); }
                                    SpawningAoE = false;
                                    SpawningMissle = false;
                                    PreAttackSpawned = false;
                                    PrepareAttack = false;
                                    SetWalkSpeed = true;
                                    spelldowncool = 0;
                                    MaxCharge = 0;
                                    CurrentCharge = 0;
                                    MagicCharged = 0;
                                    HoldingDown = false;
                                    PlayerStats.ChangeMana(-currentspell.ManaCost);
                                    FirstPersonControlerScript.SetSpeedNormal();
                                    if (MagicChargePanel) { MagicChargePanel.SetActive(false); }
                                    switch (type)
                                    {
                                        case 1:
                                            DelayAdd = 0.01f;
                                            GameObject Missle = Instantiate(MagicMisslePrefab);


                                            Missle.transform.position = MagicMissleSpawn.position;
                                            Missle.transform.rotation = MagicMissleSpawn.rotation;
                                            Missle.transform.localScale += new Vector3(ApplyCharge, ApplyCharge, ApplyCharge);
                                            
                                            Missle.GetComponent<HitRegistrator>().SetSettings(
                                                3,
                                                currentspell,
                                                transform.forward * propulsionForce);
                                            break;
                                        case 2:
                                            DelayAdd = 0.005f;
                                            GameObject AoE = Instantiate(AoEPrefab);

                                            AoE.transform.position = MagicAreaOfEffectSpawn.position;
                                            AoE.transform.rotation = MagicAreaOfEffectSpawn.rotation;
                                            AoE.transform.localScale = new Vector3(ApplyCharge, 0.05f, ApplyCharge);
                                            AoE.GetComponent<HitRegistrator>().SetSettings(
                                               3,
                                               currentspell);
                                            break;
                                        case 3:
                                            PlayerStats.health += currentspell.Change;
                                            break;
                                    }
                                    CurrentCharge = 0;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }

    void SetCombatState(int value)
    {
        CombatState = value;
    }

    void SwitchCombatStyle()
    {
        bool SwitchCheck = false;
        int oldstyle = CombatState;
        int chosen = 0;
        if (SwitchCombatPanel)
        {
            if (!WindowOpen) {
                WindowOpen = true;
                SwitchCombatPanel.SetActive(true);
            }
        }
        if (CrossPlatformInputManager.GetButton("SwitchLeft"))
        {
            SwitchCheck = true;
            SwitchDisable = Time.time + SwitchSpeed;
            CombatState = StyleOrder[0];
            int mid = StyleOrder[1];
            int left = StyleOrder[0];
            StyleOrder[0] = mid;
            StyleOrder[1] = left;
            chosen = 1;
        }
        //if (CrossPlatformInputManager.GetButton("SwitchMiddle"))
        //{
        //    CombatState = StyleOrder[1];
        //}
        if (CrossPlatformInputManager.GetButton("SwitchRight"))
        {
            SwitchCheck = true;
            SwitchDisable = Time.time + SwitchSpeed;
            CombatState = StyleOrder[2];
            int mid = StyleOrder[1];
            int right = StyleOrder[2];
            StyleOrder[2] = mid;
            StyleOrder[1] = right;
            chosen = 3;
        }

        //Set old style inactive
        if (SwitchCheck)
        {
            switch (oldstyle)
            {
                case (int)CombatStyle.Melee:
                    SwordModel.SetActive(false);
                    SwordsPanel.gameObject.SetActive(false);
                    break;
                case (int)CombatStyle.Range:
                    BowModel.SetActive(false);
                    break;
                case (int)CombatStyle.Magic:
                    StaffModel.SetActive(false);
                    ChangeSpellUI.SetMagicMode(false);
                    break;
                default:
                    Debug.LogWarning("[PLAYER] Invalid combatstyle, Model SetActive(false) failed");
                    break;
            }
            //Set new style active
            switch (CombatState)
            {
                case (int)CombatStyle.Melee:
                    SwordModel.SetActive(true);
                    SwordsPanel.gameObject.SetActive(true);
                    Debug.Log("[PLAYER] Combat: Melee Mode");
                    break;
                case (int)CombatStyle.Range:
                    BowModel.SetActive(true);
                    Debug.Log("[PLAYER] Combat: Range Mode");
                    break;
                case (int)CombatStyle.Magic:
                    StaffModel.SetActive(true);
                    ChangeSpellUI.SetMagicMode(true);
                    Debug.Log("[PLAYER] Combat: Magic Mode");
                    break;
                default:
                    Debug.LogWarning("[PLAYER] Invalid combatstyle, Model SetActive(true) failed");
                    break;
            }

            if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
            {
                SoundSelector = 10;
                AudioSource.PlayOneShot(CombatFxs[SoundSelector], AudioVolume);
            }
        }

        if (WindowOpen && SwitchCheck) {
            if (SwitchCombatPanel)
            {
                if (CombatSwitchUIScript == null)
                {
                    CombatSwitchUIScript = SwitchCombatPanel.GetComponent<CombatSwitchUI>();
                    CombatSwitchUIScript.SwitchStyles(chosen);
                }
                else
                {
                    CombatSwitchUIScript.SwitchStyles(chosen);
                }
                chosen = 0;
                WindowOpen = false;
                SwitchCombatPanel.SetActive(false);
            }
        }
    }

    void SwitchSpell()
    {
        MagicSpell++;
        if (MagicSpell > maxspells)
        {
            MagicSpell = 1;
        }
        MagicSwitchDelay = Time.time + 0.5f;
        currentspell = Spellbook.SpellbookObject.GetSpellByID(MagicSpell);
        ChangeSpellUI.SetSpellName(currentspell.SpellName);
        ChangeSpellUI.SetSpellImage(currentspell.Sprite);
        ChangeSpellUI.SetCost(currentspell.ManaCost);
        if (CombatFxs.Length > 0 && AudioSource && !AudioSource.isPlaying)
        {
            SoundSelector = 11;
            AudioSource.PlayOneShot(CombatFxs[SoundSelector], 0.1f);
        }
    }

    void SetAttackAnimation()
    {
        if (Time.time > NextAttack || Time.time > NextMeleeAttack)
        {
            NextAttack = Time.time + AttackBuildup;
            //NextMeleeAttack = NextAttack;
            HoldingDown = true;
            PrepareAttack = true;
            WaitForNextInput = false;
            //if (FirstPersonControlerScript) { FirstPersonControlerScript.CanMove = false; }
            //Set animation
            if (CombatState == 1)
            {
                switch (AttackOrder)
                {
                    case 1:
                        AttackTime = Time.time + WaitForSpawn;
                        PlayerAnimator.SetTrigger("AttackMelee01Trigger");
                        break;
                }
            }
        }
    }
}
