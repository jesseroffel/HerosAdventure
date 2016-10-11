using UnityEngine;
using System.Collections;

public class QuestInformation : MonoBehaviour {

    private string[] m_QuestDialogue;               // dialogue given when taking about the quest       (multiple allowed) 
    private string m_QuestDenyDialogue = "";        // dialogue given when quest requirements are not met
    private string m_QuestGivenDialogue = "";       // dialogue given when quest is already accepted
    private string m_QuestCompletionDialoge = "";   // dialogue given when quest is being completed
    private string m_QuestAfterwordDialogue = "";   // doalogue given after quest is completed (after word)

    private string m_QuestTitle = "";
    private int m_QuestID = 0;

    private int[] m_QuestType;                      // Quest type (multiple allowed) 
    private int m_QuestProgressionState = 0;        // current state
    private int m_QuestProgressions = 0;            // total stats of quest
    private bool m_QuestStarter = false;            // if true this quest will start the questline or just itself
    private bool m_QuestProgresser = false;         // if true this quest will progress into another quest
    private bool m_QuestCompleter = false;          // if true this quest will complete the questline or just itself

    private bool m_QuestStarted = false;            // Quest started or not
    private bool m_QuestCompleted = false;          // Quest complete or not
    private int m_QuestState = 0;                   // Current state of quest
    private int m_ProgressionQuestID = 0;           // the quest will transform to this ID as followup
    private int[] m_QuestUnlocks;                   // Completing this quest will unlock quest(s)   (multiple allowed) 

    private bool m_RequiresQuestUnlock = false;     // This quest wont start without quest(s) completed if true
    private int[] m_RequiredQuestID;                // this quest(s) need to be completed to start this one (multiple allowed) 

    private bool m_RequiresKill = false;
    private int m_RequiresKillAmount = 0;
    private int m_RequiredEnemyType = 0;

    public enum QuestState
    {
        Locked,
        Open,
        InProgression,
        Completed
    }

    public enum QuestType
    {
        Exploration,
        ItemRequirement,
        KillRequirement,
        TalkRequirement
    }

    void SetupQuestInformation()
    {

    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
