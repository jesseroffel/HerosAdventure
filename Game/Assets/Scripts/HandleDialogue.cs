using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleDialogue : MonoBehaviour {

    public GameObject DialogueWindow;
    public GameObject EndConversationIcon;
    public GameObject WaitNextLineIcon;

    public Text NamePlate;
    public Text DialogueText;
    private string TextToDialogue;

	// Use this for initialization
	void Start () {
        if (DialogueWindow == null) { Debug.LogError("GameObject DialogueWindow has reference!"); }
        if (NamePlate == null) { Debug.LogError("GameObject NamePlate has reference!"); }
        if (EndConversationIcon == null) { Debug.LogError("GameObject EndConversationIcon has reference!"); }
        if (WaitNextLineIcon == null) { Debug.LogError("GameObject WaitNextLineIcon has reference!"); }
        if (DialogueText == null) { Debug.LogError("Text DialogueText has reference!"); }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetDialogueText(string newstring)
    {
        TextToDialogue = newstring;
        DialogueText.text = TextToDialogue;
    }

    public void SetDialogueName(string newstring) { NamePlate.text = TextToDialogue; }

    void SetDialogueWindow(bool state) { if (state) { DialogueWindow.SetActive(true); } else { DialogueWindow.SetActive(false); } }

    void SetWaitIcon(bool state) { if (state) { WaitNextLineIcon.SetActive(true); } else { WaitNextLineIcon.SetActive(false); } }

    void SetEndIcon(bool state) { if (state) { WaitNextLineIcon.SetActive(true); } else { WaitNextLineIcon.SetActive(false); } }

}
