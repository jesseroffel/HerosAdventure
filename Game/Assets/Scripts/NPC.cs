using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class NPC : MonoBehaviour {

    public GameObject Model;
    public GameObject SpeakIcon;
    public Text DialogTextUI;
    public HandleDialogue DialogueHandler;

    // NPC
    string m_Name = "";
    int m_ID = 0;
    bool m_Sex = true;          // true = male, false = female
    bool m_Moving = false;
    private bool m_Interactable = true;
    private bool m_StartedTalk = false;

    //Dialog
    public string[] m_DialogStrings;

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


    void Start() {
        m_DialogueLines = m_DialogStrings.Length;
        DialogueHandler = GameObject.FindGameObjectWithTag("HUD").GetComponent<HandleDialogue>();
    }

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
        HandleDialog();
    }

    private void HandleDialog()
    {
        if (m_StartedTalk)
        {
            if (!m_SayingDialog)
            {
                if (m_DialogueLines > 0 && m_DialogStrings[0].Length > 1)
                {
                    SetIconVisibility(false);
                    m_SayingDialog = true;
                    Debug.Log("Saying Dialogue: " + m_DialogStrings[m_CurrentLine]);
                    StartCoroutine(DisplayDialog(m_DialogStrings[m_CurrentLine]));
                    
                }
                else { Debug.LogWarning("NPC: " + m_Name + " does not have any dialogue lines"); }
            }

            if (m_WaitForInput && m_GotConfirm)
            {
                Debug.Log("Dialogue new line");
                m_WaitForInput = false;
                m_GotConfirm = false;
                if (m_DialogueLines > m_CurrentLine)
                {
                    m_CurrentLine++;
                    StartCoroutine(DisplayDialog(m_DialogStrings[m_CurrentLine]));
                }
                
            }
            if (m_DialogueFinished)
            {
                Debug.Log("Dialogue ended");
                m_LineFinished = false;
                m_DialogueFinished = false;
                m_WaitForInput = false;
                m_GotConfirm = false;
                m_CurrentLine = 0;
                m_StartedTalk = false;
                m_SayingDialog = false;
                //SetDialogueVisiblity(false);
                SetIconVisibility(true);
                
            }
        }
    } 

    private IEnumerator DisplayDialog(string StringToDisplay)
    {
        //int dialogueLines = m_DialogStrings.
        int DialogLength = StringToDisplay.Length;
        DialogTextUI.text = "";

        while (m_CurrentCharacter < DialogLength)
        {
            DialogTextUI.text += StringToDisplay[m_CurrentCharacter];
            m_CurrentCharacter++;
            if (m_CurrentCharacter == DialogLength)
            {
                if (m_DialogueLines > m_CurrentLine)
                {
                    m_LineFinished = true;
                    Debug.Log("Line finished");
                    m_WaitForInput = true;
                }
                else
                {
                    m_DialogueFinished = true;
                    m_WaitForInput = true;
                    Debug.Log("Conversation finished");
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

    public bool GetIconOut() { return m_IconOut;  }

    public bool GetInteractable() { return m_Interactable; }

    public void SetStartedTalk(bool state) {if (state) { m_StartedTalk = true;} else { m_StartedTalk = false; } }

    public bool GetStartedTalk() { return m_StartedTalk; }

    public bool GetWaitForInput() { return m_WaitForInput; }

    public void SetConfirm(bool state) { if (state) { m_GotConfirm = true; } else { m_GotConfirm = false; } }

}
