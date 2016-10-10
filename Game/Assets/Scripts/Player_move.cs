using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public Rigidbody rb;
    public camera_controler cam;
    public Animator anim;
    public float speed = 1;
    public bool CanMove;

    float forwardInput, sideInput;
    float angle;

    Vector3 lastPosition;
    private NPC lastnpc;
    private bool CanSpeakWithNPC = false;
    private bool InConversation = false;

    void Start()
    {
        forwardInput = sideInput = 0;
        if (cam == null) { Debug.LogError("Player has no camera target!"); }
    }

    void Update()
    {
        GetInput();

        if (lastPosition == transform.position)
        {
            anim.SetFloat("speed", 0);
        }
        lastPosition = transform.position;
        CheckTalkInput();
    }

    void FixedUpdate()
    {
        if (CanMove && InConversation == false)
        {
            Move();
        }
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        sideInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        if (cam)
        {
            transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);

            if (sideInput < 0)
            {
                angle = -1;
                transform.RotateAround(cam.transform.position, new Vector3(0, 1, 0), angle);
            }
            if (sideInput > 0)
            {
                angle = 1;
                transform.RotateAround(cam.transform.position, new Vector3(0, 1, 0), angle);
            }
            anim.SetFloat("speed", 2);
        }
    }

    void Jump()
    {

    }

    //ander script?

    void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "NPC")
            {
                lastnpc = collision.gameObject.GetComponent<NPC>() ;
                if (lastnpc.GetInteractable() && lastnpc.GetIconOut() == false)
                {
                    lastnpc.SetIconVisibility(true);
                    CanSpeakWithNPC = true;
                }
            }
        }

    }

    void OnTriggerExit(Collider collision)
    {
        CanSpeakWithNPC = false;
        lastnpc.SetIconVisibility(false);
        lastnpc.SetStartedTalk(false);
        //lastnpc.GetComponent<NPC>().SetDialogueWindow(false);

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
    }

    
    public bool GetInConversation() { return InConversation; }
}
