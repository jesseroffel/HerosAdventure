using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(GameObject))]
public class NPC : MonoBehaviour {

    public Text DialogTextUI;
    public GameObject Model;

    string m_Name = "";
    bool m_Sex = true;          // true = male, false = female
    bool m_Moving = false;
    bool m_Interactable = true;
    bool m_Interacting = false;

    //Dialog
    public string[] m_DialogStrings;
    private float TimeBetweenCharacter = 0.05f;
    private bool m_SayingDialog = false;

	// Use this for initialization
	void Start () {
	
	}

    void SetNPCWithID(int id)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (!m_SayingDialog)
            {
                m_SayingDialog = true;
                StartCoroutine(DisplayDialog(m_DialogStrings[0]));
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
}
