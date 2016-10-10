using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class NPC : MonoBehaviour {

    public GameObject Model;
    public GameObject SpeakIcon;
    private HandleDialogue DialogueHandler;


    // NPC
    string m_Name = "";
    int m_ID = 0;
    bool m_Sex = true;          // true = male, false = female
    bool m_Moving = false;
    private bool m_Interactable = true;
    private bool m_StartedTalk = false;

    //Dialog
    public string[] m_DialogStrings;
    private string tempDialogue;

    private float TimeBetweenCharacter = 0.05f;
    private int m_DialogueLines = 0;
    private int m_CurrentLine = 0;
    private int m_CurrentCharacter = 0;

    private bool m_SayingDialog = false;
    private bool m_IconOut = false;

    private bool m_LineFinished = false;
    private bool m_DialogueFinished = false;
    private bool m_WaitForInput = false;
    private bool m_GotConfirm = false;

    private bool OneTalkOnly = false;
    private bool CheckDelay = false;
    private float WaitDelay = 0;

    private bool ReleasePlayer = false;


    void SetNPCWithID(int id)
    {
        // array Npclistclass.getinformationfromid(id);
        // m_name = array[0]
        // sex array1
           //etc
    }

    void SetMoodSpeech(int mood)
    {
        switch(mood)
        {
            case 1:
                TimeBetweenCharacter = 0.13f;
                break;
            case 2:
                TimeBetweenCharacter = 0.22f;
                break;
            case 3:
                TimeBetweenCharacter = 0.3f;
                break;
            default:
                TimeBetweenCharacter = 0.08f;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Interactable)
        {
            if (CheckDelay)
            {
                if (Time.time > WaitDelay)
                {
                    m_Interactable = true;
                    CheckDelay = false;
                    Debug.Log("m_StartedTalk: " + m_StartedTalk);
                }
            }
            else
            {
                HandleDialog();
            }
        }
    }

    private void HandleDialog()
    {
        if (m_StartedTalk)
        {
            if (DialogueHandler == null) { DialogueHandler = GameObject.FindGameObjectWithTag("HUD").GetComponent<HandleDialogue>(); }
            if (!m_SayingDialog)
            {
                m_DialogueLines = m_DialogStrings.Length;
                if (m_DialogueLines > 0 && m_DialogStrings[0].Length > 1)
                {
                    SetIconVisibility(false);
                    DialogueHandler.SetDialogueWindow(true);
                    m_SayingDialog = true;
                    StartCoroutine(DisplayDialog(m_DialogStrings[m_CurrentLine]));

                }
                else
                {
                    Debug.LogWarning("NPC: " + m_Name + " does not have any dialogue lines");
                    m_StartedTalk = false;
                }
            }

            if (m_WaitForInput && m_GotConfirm && m_DialogueFinished == false)
            {
                m_LineFinished = false;
                m_WaitForInput = false;
                m_GotConfirm = false;
                m_CurrentCharacter = 0;
                DialogueHandler.SetWaitIcon(false);
                if (m_DialogueLines > m_CurrentLine)
                {
                    m_CurrentLine++;
                    StartCoroutine(DisplayDialog(m_DialogStrings[m_CurrentLine]));
                }
                
            }
            if (m_DialogueFinished && m_GotConfirm)
            {
                if (OneTalkOnly) { m_Interactable = false; }
                m_LineFinished = false;
                m_DialogueFinished = false;
                m_WaitForInput = false;
                m_GotConfirm = false;
                m_CurrentLine = 0;
                m_CurrentCharacter = 0;
                m_StartedTalk = false;
                m_SayingDialog = false;
                DialogueHandler.SetDialogueWindow(false);
                SetIconVisibility(true);
                DialogueHandler.SetEndIcon(false);

                CheckDelay = true;
                WaitDelay = Time.time + 2.5f;
                ReleasePlayer = false;
            }
        }
    } 

    private IEnumerator DisplayDialog(string StringToDisplay)
    {
        Debug.Log("DisplayDialog: " + StringToDisplay);
        //int dialogueLines = m_DialogStrings.
        int DialogLength = StringToDisplay.Length;
        tempDialogue = "";
        DialogueHandler.SetDialogueText(tempDialogue);
        while (m_CurrentCharacter < DialogLength)
        {
            tempDialogue += StringToDisplay[m_CurrentCharacter];
            DialogueHandler.SetDialogueText(tempDialogue);

            m_CurrentCharacter++;
            if (m_CurrentCharacter == DialogLength)
            {
                if (m_DialogueLines - 1 == m_CurrentLine)
                {
                    m_DialogueFinished = true;
                    m_WaitForInput = true;
                    Debug.Log("Conversation finished");
                    DialogueHandler.SetEndIcon(true);
                }
                else
                {
                    m_LineFinished = true;
                    m_WaitForInput = true;
                    Debug.Log("Line finished");
                    DialogueHandler.SetWaitIcon(true);
                }
            }

            if (m_CurrentCharacter < DialogLength)
            {
                yield return new WaitForSeconds(TimeBetweenCharacter);
            } else
            {
                break;
            }
        }
    }

    public void SetIconVisibility(bool state)
    {
        if (state) {
            m_IconOut = true;
            if (SpeakIcon) { SpeakIcon.SetActive(true); }
        }
        else {
            m_IconOut = false;
            if (SpeakIcon) { SpeakIcon.SetActive(false); }
        }
    }

    public void SetDialogueWindow(bool state) { if (state) { DialogueHandler.SetDialogueWindow(true); } else { DialogueHandler.SetDialogueWindow(false); } }

    public bool GetIconOut() { return m_IconOut;  }

    public bool GetInteractable() { return m_Interactable; }

    public void SetStartedTalk(bool state) {if (state) { m_StartedTalk = true;} else { m_StartedTalk = false; } }

    public bool GetStartedTalk() { return m_StartedTalk; }

    public bool GetWaitForInput() { return m_WaitForInput; }

    public void SetConfirm(bool state) { if (state) { m_GotConfirm = true; } else { m_GotConfirm = false; } }
    
    public bool GetReleasePlayer() { return ReleasePlayer; }

}
