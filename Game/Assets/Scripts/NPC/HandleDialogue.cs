using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleDialogue : MonoBehaviour {

    public GameObject DialogueWindow;
    public GameObject EndConversationIcon;
    public GameObject WaitNextLineIcon;

    public Text NamePlate;
    public Text QuestPlate;
    public Text DialogueText;

    public GameObject TalkUI;

    private string TextToDialogue;
    private bool IconActive = false;
    private bool EndIconActive = false;
    private bool DownTime = false;
    private float BlinkTime = 0.0f;

    private bool QuestIcon = false;
    private bool QuestOver = false;

	// Use this for initialization
	void Start () {
        if (DialogueWindow == null) { Debug.LogError("GameObject DialogueWindow no has reference!"); }
        if (NamePlate == null) { Debug.LogError("GameObject NamePlate no has reference!"); }
        if (EndConversationIcon == null) { Debug.LogError("GameObject EndConversationIcon no has reference!"); }
        if (WaitNextLineIcon == null) { Debug.LogError("GameObject WaitNextLineIcon no has reference!"); }
        if (DialogueText == null) { Debug.LogError("Text DialogueText no has reference!"); }
        if (QuestPlate == null) { Debug.LogError("Text QuestPlate no has reference!"); }
        if (TalkUI == null) { Debug.LogError("GameObject TalkUI no has reference!"); }
    }
	
	// Update is called once per frame
	void Update () {
        BlinkIcon();
    }

    public void BlinkIcon()
    {
        if (IconActive)
        {
            if (Time.time > BlinkTime)
            {
                BlinkTime = Time.time + 0.5f;
                if (DownTime)
                {
                    DownTime = false;
                    if (EndIconActive)
                    {
                        EndConversationIcon.SetActive(true);
                    }
                    else
                    {
                        WaitNextLineIcon.SetActive(true);
                    }
                }
                else
                {
                    DownTime = true;
                    if (EndIconActive)
                    {
                        EndConversationIcon.SetActive(false);
                    }
                    else
                    {
                        WaitNextLineIcon.SetActive(false);
                    }
                }
            }
        }
    }

    public void SetDialogueText(string newstring)
    {
        TextToDialogue = newstring;
        DialogueText.text = TextToDialogue;
    }

    public void SetDialogueName(string newstring) { NamePlate.text = newstring; }

    public void SetQuestName(string newstring) { QuestPlate.text = newstring; }
    

    public void SetDialogueWindow(bool state) { if (state) { DialogueWindow.SetActive(true); } else { DialogueWindow.SetActive(false); } }

    public void SetWaitIcon(bool state) {
        if (state) { WaitNextLineIcon.SetActive(true); IconActive = true; } else { WaitNextLineIcon.SetActive(false); IconActive = false; }
    }

    public void SetEndIcon(bool state) {
        if (state) { EndConversationIcon.SetActive(true); IconActive = true; EndIconActive = true;  }
        else { EndConversationIcon.SetActive(false); IconActive = false; EndIconActive = false; }
    }

    public void SetQuestIcon(bool state)
    {
        if (state) { QuestIcon = true; }
        else { QuestIcon = false; }
    }

    public void SetTextUI(bool state)
    {
        if (state) { TalkUI.SetActive(true); } else { TalkUI.SetActive(false); }
    }
}
