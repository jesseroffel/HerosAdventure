using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CombatSystem : MonoBehaviour {
    public FirstPersonControler FirstPersonControlerScript;
    public Animator PlayerAnimator;
    public Transform HitRegBlock;
    public GameObject ArrowSpawn;
    public GameObject ArrowPrefab; 
    public GameObject SwordModel;
    public GameObject StaffModel;
    public GameObject BowModel;
    //public Transform HandPosition;
    //public Transform BackPosition01;
    //public Transform BackPosition02;


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
    
    // UI
    public Text StrenghText;
    public GameObject StrenghPanel;
    private float BowStrengh = 0;
    private bool HoldingDown = false;

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
        if (StrenghText == null) { Debug.LogError("Text 'StrenghText' is null, set reference"); }
        if (StrenghPanel == null) { Debug.LogError("GameObject 'StrenghPanel' is null, set reference"); }
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
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            HoldingDown = false;
        }

        if (CrossPlatformInputManager.GetButton("SwitchCombat") && Time.time > SwitchDisable)
        {
            SwitchCombatStyle();
        }

        if (PrepareAttack)
        {
            // Melee
            if (Time.time > AttackTime)
            {
                Attacking = true;
                if (CombatState != (int)CombatStyle.Range) { PrepareAttack = false; }
                NextAttack = Time.time + Attack01Swing;
                //Vector3 Addpos = transform.position + (transform.forward);
                
                switch (CombatState)
                {
                    case (int)CombatStyle.Melee:
                        Transform HitDec = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                        HitDec.transform.parent = transform;
                        switch (AttackOrder)
                        {
                            case 1:
                                
                                HitDec.transform.position += new Vector3(0, 0.4f, 0);
                                HitDec.GetComponent<HitRegistrator>().SetSettings(1,Attack01Swing, AttackPower, transform.forward * propulsionForce);
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                        break;
                    case (int)CombatStyle.Range:
                        if (HoldingDown)
                        {
                            if (StrenghPanel) {
                                if (StrenghPanel.activeSelf == false) { StrenghPanel.SetActive(true); }
                            }
                            string Text = "";
                            if (BowStrengh < 1) {
                                BowStrengh += 0.01f;
                                if (BowStrengh > 1) { BowStrengh = 1; }
                                
                            }
                            Text = BowStrengh.ToString("F2");
                            if (StrenghText) { StrenghText.text = Text; }
                        } else
                        {
                            BowStrengh = 0;
                            PrepareAttack = false;
                            GameObject Projectile = Instantiate(ArrowPrefab);
                            Projectile.transform.position = ArrowSpawn.transform.position;
                            Projectile.transform.rotation = Quaternion.identity;
                            Rigidbody rb = Projectile.GetComponent<Rigidbody>();
                            rb.velocity = (ArrowSpawn.transform.forward * 20) * BowStrengh;

                            Projectile.GetComponent<HitRegistrator>().SetSettings(2, 5, 10, transform.forward * propulsionForce);
                            if (StrenghPanel) { StrenghPanel.SetActive(false); }
                            
                        }
                        
                        break;
                    case (int)CombatStyle.Magic:
                        break;
                }
            }
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
        bool SwitchCheck = false;
        int oldstyle = CombatState;
        if (CrossPlatformInputManager.GetButton("SwitchLeft"))
        {
            SwitchCheck = true;
            SwitchDisable = Time.time + SwitchSpeed;
            CombatState = StyleOrder[0];
            int mid = StyleOrder[1];
            int left = StyleOrder[0];
            StyleOrder[0] = mid;
            StyleOrder[1] = left;
        }
        if (CrossPlatformInputManager.GetButton("SwitchMiddle"))
        {
            CombatState = StyleOrder[1];
        }
        if (CrossPlatformInputManager.GetButton("SwitchRight"))
        {
            SwitchCheck = true;
            SwitchDisable = Time.time + SwitchSpeed;
            CombatState = StyleOrder[2];
            int mid = StyleOrder[1];
            int right = StyleOrder[2];
            StyleOrder[2] = mid;
            StyleOrder[1] = right;
        }

        //Debug.Log("Left: " + StyleOrder[0] + " Mid: " + StyleOrder[1] + " Right: " + StyleOrder[2]);

        //CombatState++;
        //if (CombatState == 4) { CombatState = 1; }

        //Set old weapon inactive
        if (SwitchCheck)
        {
            switch (oldstyle)
            {
                case (int)CombatStyle.Melee:
                    SwordModel.SetActive(false);
                    break;
                case (int)CombatStyle.Range:
                    BowModel.SetActive(false);
                    break;
                case (int)CombatStyle.Magic:
                    StaffModel.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("[PLAYER] Invalid combatstyle, Model SetActive(false) failed");
                    break;
            }
            //Set new weapon active
            switch (CombatState)
            {
                case (int)CombatStyle.Melee:
                    SwordModel.SetActive(true);
                    Debug.Log("[PLAYER] Combat: Melee Mode");
                    break;
                case (int)CombatStyle.Range:
                    BowModel.SetActive(true);
                    Debug.Log("[PLAYER] Combat: Range Mode");
                    break;
                case (int)CombatStyle.Magic:
                    StaffModel.SetActive(true);
                    Debug.Log("[PLAYER] Combat: Magic Mode");
                    break;
                default:
                    Debug.LogWarning("[PLAYER] Invalid combatstyle, Model SetActive(true) failed");
                    break;
            }
        }  
    }

    void SetAttackAnimation()
    {
        NextAttack = Time.time + AttackBuildup;
        HoldingDown = true;
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
