using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class FirstPersonControler : MonoBehaviour
{
    public float speed;
    public float jumpVelocity;
    public bool CanMove;

    Rigidbody rb;

    private NPCController lastnpc;
    public HandleDialogue DialogueManager;
    private bool CanSpeakWithNPC = false;
    private bool InteractingWithNPC = false;
    private bool InConversation = false;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        if (DialogueManager == null) { DialogueManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HandleDialogue>(); }
    }

    void Update()
    { 
        if (CanMove)
        {
            float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float sideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            transform.Translate(sideward, 0, forward);
        }
        CheckTalkInput();
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.up * jumpVelocity);
        }       
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "NPC")
            {
                InConversation = true;
                lastnpc = collision.gameObject.GetComponent<NPCController>();
                if (lastnpc.GetInteractable() && lastnpc.GetIconOut() == false)
                {
                    //lastnpc.SetIconVisibility(true);
                    lastnpc.SetPlayerInRange(true);
                    CanSpeakWithNPC = true;
                    DialogueManager.SetTextUI(true);
                }
                InteractingWithNPC = true;
            }
        }


    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "NPC")
        {
            CanSpeakWithNPC = false;
            InConversation = false;
            //lastnpc.SetIconVisibility(false);
            lastnpc.SetPlayerInRange(false);
            lastnpc.SetStartedTalk(false);
            DialogueManager.SetTextUI(false);

            //lastnpc.GetComponent<NPC>().SetDialogueWindow(false);
        }
    }

    void CheckTalkInput()
    {
        if (CanSpeakWithNPC)
        {
            if (lastnpc.GetStartedTalk() == false)
            {
                if (CrossPlatformInputManager.GetButton("Fire1"))
                {
                    CanMove = false;
                    lastnpc.SetStartedTalk(true);
                    DialogueManager.SetTextUI(false);
                }
            }
            if (lastnpc.GetWaitForInput() == true)
            {
                if (CrossPlatformInputManager.GetButton("Fire1"))
                {
                    lastnpc.SetConfirm(true);
                }
            }
            if (lastnpc.GetReleasePlayer())
            {
                CanMove = true;
                lastnpc.SetReleasePlayer(false);
            }
        }
        //if (InteractingWithNPC && ) MAKE INTERACTION DIALOGUE WITH IGNORING NPC
    }


    public bool GetInConversation() { return InConversation; }
}
