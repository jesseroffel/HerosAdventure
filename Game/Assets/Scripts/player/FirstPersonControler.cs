using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class FirstPersonControler : MonoBehaviour
{
    public float speed;
    public bool CanMove;

    //Camera cam;

    private NPCController lastnpc;
    private bool CanSpeakWithNPC = false;
    private bool InteractingWithNPC = false;
    private bool InConversation = false;

    void Update() { 
        if (CanMove && InConversation == false) {
            float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float sideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            transform.Translate(sideward, 0, forward);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "NPC")
            {
                lastnpc = collision.gameObject.GetComponent<NPCController>();
                if (lastnpc.GetInteractable() && lastnpc.GetIconOut() == false)
                {
                    lastnpc.SetIconVisibility(true);
                    CanSpeakWithNPC = true;

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
            lastnpc.SetIconVisibility(false);
            lastnpc.SetStartedTalk(false);
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
                    lastnpc.SetStartedTalk(true);
                    CanMove = false;
                    InConversation = true;
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
                InConversation = false;
                lastnpc.SetReleasePlayer(false);
            }
        }
        //if (InteractingWithNPC && ) MAKE INTERACTION DIALOGUE WITH IGNORING NPC
    }


    public bool GetInConversation() { return InConversation; }
}
