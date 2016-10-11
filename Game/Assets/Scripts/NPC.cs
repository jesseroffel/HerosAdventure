using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class NPC : MonoBehaviour {

    public GameObject Model;
    public GameObject SpeakIcon;
    private HandleDialogue DialogueHandler;


    // NPC
    public string m_npcName = "";
    public int m_NPCID = 0;            // NPC ID
    public int m_INFOID = 0;           // NPC Information from DB with ID
    private bool m_Sex = true;         // true = male, false = female
    private bool m_Moving = false;
    private bool m_Interactable = true;
    private bool m_StartedTalk = false;

    //Quest
    private string m_QuestTitle = "";
    private int m_QuestID = 0;
    private int m_QuestProgression = 0;
    private bool m_QuestAssosiated = false;
    private bool m_QuestStarter = false;
    private bool m_QuestCompleted = false;
    

    //Dialog
    public string[] m_Dialogue;
    private string m_QuestDeny;
    private string m_QuestGiven;
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

    // Update is called once per frame
    void Update()
    {
        if (m_Interactable)
        {
            if (CheckDelay)
            {
                if (Time.time > WaitDelay)
                {
                    CheckDelay = false;
                    m_StartedTalk = false;
                    SetIconVisibility(true);
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
                m_DialogueLines = m_Dialogue.Length;
                if (m_DialogueLines > 0 && m_Dialogue[0].Length > 1)
                {
                    Debug.Log("Displaying dialogue from npc " + m_npcName + "..");
                    ReleasePlayer = false;
                    m_SayingDialog = true;
                    SetIconVisibility(false);
                    DialogueHandler.SetDialogueName(m_npcName);
                    DialogueHandler.SetQuestName(m_QuestTitle);
                    DialogueHandler.SetDialogueWindow(true);
                    StartCoroutine(DisplayDialog(m_Dialogue[m_CurrentLine]));
                }
                else
                {
                    Debug.LogWarning("NPC: " + m_npcName + " does not have any dialogue lines");
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
                    StartCoroutine(DisplayDialog(m_Dialogue[m_CurrentLine]));
                }
                
            }
            if (m_DialogueFinished && m_GotConfirm)
            {
                ResetDialogue();
            }
        }
    } 

    private IEnumerator DisplayDialog(string StringToDisplay)
    {
        Debug.Log("Displaying: " + StringToDisplay);
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
                    Debug.Log("Conversation finished, waiting for confirm..");
                    DialogueHandler.SetEndIcon(true);
                }
                else
                {
                    m_LineFinished = true;
                    m_WaitForInput = true;
                    Debug.Log("Line finished, waiting for confirm..");
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

    void ResetDialogue()
    {
        if (OneTalkOnly) { m_Interactable = false; } else { m_Interactable = true; }

        m_LineFinished = false;
        m_DialogueFinished = false;
        m_WaitForInput = false;
        m_GotConfirm = false;
        m_CurrentLine = 0;
        m_CurrentCharacter = 0;
        m_StartedTalk = false;
        m_SayingDialog = false;
        DialogueHandler.SetDialogueWindow(false);
        DialogueHandler.SetEndIcon(false);

        CheckDelay = true;
        WaitDelay = Time.time + 2.5f;
        ReleasePlayer = true;
        
    }

    public void SetDialogueWindow(bool state) { if (state) { DialogueHandler.SetDialogueWindow(true); } else { DialogueHandler.SetDialogueWindow(false); } }

    public bool GetIconOut() { return m_IconOut;  }

    public bool GetInteractable() { return m_Interactable; }

    public void SetStartedTalk(bool state) {if (state) { m_StartedTalk = true;} else { m_StartedTalk = false; } }

    public bool GetStartedTalk() { return m_StartedTalk; }

    public bool GetWaitForInput() { return m_WaitForInput; }

    public void SetConfirm(bool state) { if (state) { m_GotConfirm = true; } else { m_GotConfirm = false; } }
    
    public bool GetReleasePlayer() { return ReleasePlayer; }

    public void SetReleasePlayer(bool state) { if (state) { ReleasePlayer = true; } else { ReleasePlayer = false; } }
    


    void SetMoodSpeech(int mood)
    {
        switch (mood)
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
}
