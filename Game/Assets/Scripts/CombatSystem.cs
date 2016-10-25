using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CombatSystem : MonoBehaviour {
    public FirstPersonControler FirstPersonControlerScript;
    public Animator PlayerAnimator;
    public Transform HitRegBlock;
    public Transform SwordPrefab;
    public Transform HandPosition;
    public Transform BackPosition01;
    public Transform BackPosition02;


    public float Attack01Swing = 0.5f;
    public float AttackBuildup = 0.5f;

    private float propulsionForce = 10.0f;
    private float NextAttack = 0.0f;
    private float WaitForSpawn = 0.3f;
    private float AttackTime = 0.0f;
    private bool PrepareAttack = false;
    private bool Attacking = false;
    private int CombatState = 1;
    private int AttackOrder = 0;
    private bool HandEmpty = true;

    private float AttackPower = 15.0f;

    private Quaternion SwordRot = new Quaternion(50, 0, 0, 0);

    enum CombatStyle { NoCombat = 0,  Melee = 1 , Range = 2, Magic = 3};

    // Use this for initialization
    void Start() {
        //myTransform = gameObject.position;
        if (PlayerAnimator == null) { Debug.LogError("Animator 'PlayerAnimator' is null, set reference"); }
        if (HitRegBlock == null) { Debug.LogError("Transform 'HitRegBlock' is null, set reference"); }
        if (FirstPersonControlerScript == null) { Debug.LogError("Player_move 'playermovescript' is null, set reference"); }
        SwitchCombatStyle();
    }
	
	// Update is called once per frame
	void Update () {

	    if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > NextAttack)
        {
            if (FirstPersonControlerScript.GetInConversation()) {
                //Debug.Log("Conversation confirm, make animation of this!");
            } else
            {
                SetAttackAnimation();
            }
        }

        if (CrossPlatformInputManager.GetButton("Fire3") && Time.time > NextAttack)
        {
            SwitchCombatStyle();
        }
        if (PrepareAttack)
        {
            // Melee
            if (Time.time > AttackTime)
            {
                Attacking = true;
                PrepareAttack = false;
                NextAttack = Time.time + Attack01Swing;
                //Vector3 Addpos = transform.position + (transform.forward);
                Transform HitDec = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                switch (CombatState)
                {
                    case (int)CombatStyle.Melee:
                        switch (AttackOrder)
                        {
                            case 1:
                                HitDec.transform.position += new Vector3(0, 0.4f, 0);
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                        break;
                    case (int)CombatStyle.Range:
                        break;
                    case (int)CombatStyle.Magic:
                        break;
                }
                HitDec.transform.parent = transform;
                //HitDec.transform.rotation = transform.rotation;
                HitDec.GetComponent<HitRegistrator>().SetSettings(Attack01Swing, AttackPower, transform.forward * propulsionForce);
                //
            }
            // Range
            // Magic
        }
        if (Attacking)
        {
            if (Time.time > NextAttack)
            {
                Attacking = false;
                AttackOrder = 0;
                if (FirstPersonControlerScript) { FirstPersonControlerScript.CanMove = true; }
            }
        }
    }

    void SetCombatState(int value)
    {
        CombatState = value;
    }

    void SwitchCombatStyle()
    {
        switch (CombatState)
        {
            case (int)CombatStyle.Melee:
                if (HandEmpty)
                {
                    HandEmpty = false;
                    Transform Sword = Instantiate(SwordPrefab);
                    Sword.transform.position = HandPosition.position;
                    Sword.transform.parent = HandPosition;
                    //Sword.transform.localRotation = Quaternion.identity;
                    Debug.Log("[PLAYER] Combat: Melee Mode");
                }
                break;
        }
    }

    void SetAttackAnimation()
    {
        NextAttack = Time.time + AttackBuildup;
        PrepareAttack = true;
        if (FirstPersonControlerScript) { FirstPersonControlerScript.CanMove = false; }
        //Set animation
        if (CombatState == 1)
        {
            AttackOrder++;
            AttackTime = Time.time + WaitForSpawn;
            switch (AttackOrder)
            {
                case 1:
                    PlayerAnimator.SetTrigger("AttackMelee01Trigger");
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}
