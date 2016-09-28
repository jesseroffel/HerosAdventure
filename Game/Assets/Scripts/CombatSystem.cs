using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CombatSystem : MonoBehaviour {

    public Animator PlayerAnimator;
    public Transform HitRegBlock;
    public float AttackRate = 0.5f;
    private Transform myTransform;
    private float NextAttack = 0.0f;
    private float WaitForSpawn = 0.35f;
    private float SpawnCollitionBlock = 0.0f;
    private bool Countdown = false;

    // Use this for initialization
    void Start() {
        //myTransform = gameObject.position;
        if (PlayerAnimator == null) { Debug.LogError("Animator 'PlayerAnimator' is null, set reference"); }
        if (HitRegBlock == null) { Debug.LogError("Transform 'HitRegBlock' is null, set reference"); }
    }
	
	// Update is called once per frame
	void Update () {

	    if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > NextAttack)
        {
            NextAttack = Time.time + AttackRate;
            PlayerAnimator.SetTrigger("AttackMelee01Trigger");
            Countdown = true;
            SpawnCollitionBlock = Time.time + WaitForSpawn;
        }
        if (Countdown)
        {
            if (Time.time > SpawnCollitionBlock)
            {
                Countdown = false;
                Debug.Log("Spawn block");
                Instantiate(HitRegBlock, new Vector3(0.15f, 0.7f, 0.5f), Quaternion.identity);
            }
        }
	}
}
