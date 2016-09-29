using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CombatSystem : MonoBehaviour {
    public Player_move playermovescript;
    public Animator PlayerAnimator;
    public Transform HitRegBlock;
    public float AttackSwing = 0.5f;
    public float AttackBuildup = 0.5f;
    private Transform myTransform;
    private float NextAttack = 0.0f;
    private float WaitForSpawn = 0.35f;
    private float AttackTime = 0.0f;
    private bool PrepareAttack = false;
    private bool Attacking = false;
    private float PlayerSpeed = 0;
    private int CombatState = 1;
    private int AttackOrder = 0;

    private float AttackPower = 25.0f;

    enum CombatStyle { NoCombat = 0,  Melee = 1 , Range = 2, Magic = 3};

    // Use this for initialization
    void Start() {
        //myTransform = gameObject.position;
        if (PlayerAnimator == null) { Debug.LogError("Animator 'PlayerAnimator' is null, set reference"); }
        if (HitRegBlock == null) { Debug.LogError("Transform 'HitRegBlock' is null, set reference"); }
        if (playermovescript == null) { Debug.LogError("Player_move 'playermovescript' is null, set reference"); }
    }

    void SetCombatState(int value)
    {
        CombatState = value;
    }

    void SwitchCombatStyle()
    {

    }
	
	// Update is called once per frame
	void Update () {

	    if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > NextAttack)
        {
            NextAttack = Time.time + AttackBuildup;
            PrepareAttack = true;
            if (playermovescript) {  playermovescript.CanMove = true; }
            //Set animation
            switch(CombatState) {
                case (int)CombatStyle.Melee:
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
                    break;
                case (int)CombatStyle.Range:
                    break;
                case (int)CombatStyle.Magic:
                    break;
            }
        }
        if (PrepareAttack)
        {
            // Melee
            if (Time.time > AttackTime)
            {
                Attacking = true;
                PrepareAttack = false;
                NextAttack = Time.time + AttackSwing;
                Transform HitDec = (Transform)Instantiate(HitRegBlock, transform.position + (transform.forward), transform.rotation);
                switch (CombatState)
                {
                    case (int)CombatStyle.Melee:
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
                        break;
                    case (int)CombatStyle.Range:
                        break;
                    case (int)CombatStyle.Magic:
                        break;
                }
                HitDec.transform.position += new Vector3(0, 0.4f, 0);
                HitDec.transform.parent = transform.FindChild("guy") ;
                //HitDec.transform.rotation = transform.rotation;
                HitDec.GetComponent<HitRegistrator>().Init(0.5f, AttackPower);
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
                if (playermovescript) { playermovescript.CanMove = true; }
            }
        }
    }
}
