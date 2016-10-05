using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class NPC : MonoBehaviour {

    public GameObject Model;
    public GameObject SpeakIcon;
    public Text DialogTextUI;
    public bool m_Interactable = true;
    public bool m_IconOut = false;
    public bool m_Interacting = false;
    public bool m_InteractCollider = false;

    // NPC
    string m_Name = "";
    int m_ID = 0;
    bool m_Sex = true;          // true = male, false = female
    bool m_Moving = false;

    //Dialog
    public string[] m_DialogStrings;
    private float TimeBetweenCharacter = 0.05f;
    private bool m_SayingDialog = false;



    void SetNPCWithID(int id)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Interacting)
        {
            if (!m_SayingDialog)
            {
                if (m_DialogStrings[0].Length > 1)
                {
                    m_SayingDialog = true;
                    Debug.Log("Start Dialog:" + m_DialogStrings[0]);
                    //StartCoroutine(DisplayDialog(m_DialogStrings[0]));
                }

            }
            
        }
    }
    private IEnumerator DisplayDialog(string StringToDisplay)
    {
        int DialogLength = StringToDisplay.Length;
        int currentcharacter = 0;
        DialogTextUI.text = "";

        while (currentcharacter > DialogLength)
        {
            DialogTextUI.text += StringToDisplay[currentcharacter];
            currentcharacter++;

            //if(currentcharacter < StringToDisplay)
            //{

            //}
        }

        yield return new WaitForSeconds(TimeBetweenCharacter);
    }

    public void SetIconVisibility(bool state)
    {
        if (state) {
            m_IconOut = true;
            if (SpeakIcon) { SpeakIcon.SetActive(true); }
            Debug.Log("Speak Icon Appearing");
        }
        else {
            m_IconOut = false;
            if (SpeakIcon) { SpeakIcon.SetActive(false); }
            Debug.Log("Speak Icon Disappearing");
        }
    }

    public bool GetInteractCollider() { return m_InteractCollider; }

    public void SetInteractCollider(bool state)
    {
        if (state) { m_InteractCollider = true; Debug.Log("m_InteractCollider true"); }
        else{ m_InteractCollider = false; Debug.Log("m_InteractCollider false"); }
    }
}
