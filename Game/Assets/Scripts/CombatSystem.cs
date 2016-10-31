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

    //Switching
    public float SwitchSpeed = 1.0f;
    private float SwitchDisable = 0;
    private bool FinishSwitch = false;
    private bool SwitchChosen = false;
    private int SwitchChosenOption = 0;
    private int[] StyleOrder = { 2, 1, 3 };

    private float AttackPower = 15.0f;

    private Quaternion SwordRot = new Quaternion(50, 0, 0, 0);

    enum CombatStyle { NoCombat = 0,  Melee = 1 , Range = 2, Magic = 3};

    // Use this for initialization
    void Start() {
        //myTransform = gameObject.position;
        if (PlayerAnimator == null) { Debug.LogError("Animator 'PlayerAnimator' is null, set reference"); }
        if (HitRegBlock == null) { Debug.LogError("Transform 'HitRegBlock' is null, set reference"); }
        if (FirstPersonControlerScript == null) { Debug.LogError("Player_move 'playermovescript' is null, set reference"); }
       // SwitchCombatStyle();
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

        if (CrossPlatformInputManager.GetButton("Fire3") && Time.time > SwitchDisable)
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
        if (CrossPlatformInputManager.GetButton("SwitchLeft"))
        {
            CombatState = StyleOrder[0];
            int mid = StyleOrder[1];
            int left = StyleOrder[0];
            StyleOrder[0] = mid;
            StyleOrder[1] = left;

            Debug.Log("SwitchLeft: " + StyleOrder[0]);
        }
        if (CrossPlatformInputManager.GetButton("SwitchMiddle"))
        {
            CombatState = StyleOrder[1];
            Debug.Log("SwitchMiddle: " + StyleOrder[1]);
        }
        if (CrossPlatformInputManager.GetButton("SwitchRight"))
        {
            CombatState = StyleOrder[2];
            int mid = StyleOrder[1];
            int right = StyleOrder[2];
            StyleOrder[2] = mid;
            StyleOrder[1] = right;
            Debug.Log("SwitchRight: " + StyleOrder[2]);
        }
        SwitchDisable = Time.time + SwitchSpeed;
        Debug.Log("Left: " + StyleOrder[0] + " Mid: " + StyleOrder[1] + " Right: " + StyleOrder[2]);
        //CombatState++;
        //if (CombatState == 4) { CombatState = 1; }
        /*
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
                }
                Debug.Log("[PLAYER] Combat: Melee Mode");
                break;
            case (int)CombatStyle.Range:
                if (HandEmpty)
                {
                    HandEmpty = false;
                    Transform Sword = Instantiate(SwordPrefab);
                    Sword.transform.position = HandPosition.position;
                    Sword.transform.parent = HandPosition;
                    //Sword.transform.localRotation = Quaternion.identity;
                }
                Debug.Log("[PLAYER] Combat: Range Mode");
                break;
            case (int)CombatStyle.Magic:
                if (HandEmpty)
                {
                    HandEmpty = false;
                    Transform Sword = Instantiate(SwordPrefab);
                    Sword.transform.position = HandPosition.position;
                    Sword.transform.parent = HandPosition;
                    //Sword.transform.localRotation = Quaternion.identity;
                }
                Debug.Log("[PLAYER] Combat: Magic Mode");
                break;
        }
        */
        
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
