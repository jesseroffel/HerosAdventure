using UnityEngine;
using System.Collections;

public class QuestInformation : MonoBehaviour {

    private int m_QuestID { get; set; }
    private string m_QuestTitle { get; set; }

    private int[] m_QuestType { get; set; }                     // Quest type (multiple allowed) 
    private bool m_QuestStarter { get; set; }                   // if true this quest will start the questline or just itself
    private bool m_QuestProgresser { get; set; }                // if true this quest will progress into another quest
    private bool m_QuestCompleter { get; set; }                 // if true this quest will complete the questline or just itself

    private bool m_QuestStarted { get; set; }                   // Quest started or not
    private bool m_QuestCompleted { get; set; }                 // Quest complete or not
    private int m_QuestProgressionState { get; set; }           // current state
    private int m_QuestTotalProgressions { get; set; }          // total stats of quest                    // Current state of quest
    private int m_ProgressionQuestID { get; set; }              // the quest will transform to this ID as followup
    private int[] m_QuestUnlocks { get; set; }                  // Completing this quest will unlock quest(s)   (multiple allowed) 

    private bool m_RequiresQuestUnlock { get; set; }            // This quest wont start without quest(s) completed if true
    private int[] m_RequiredQuestID { get; set; }               // this quest(s) need to be completed to start this one (multiple allowed) 

    private bool m_RequiresKill { get; set; }
    private int[] m_RequiresKillAmount { get; set; }
    private int[] m_RequiredEnemyID { get; set; }

    private bool m_RequiresItem { get; set; }
    private int[] m_RequiredItemID { get; set; }

    private string[] m_QuestDialogue { get; set; }              // dialogue given when taking about the quest       (multiple allowed) 
    private string m_QuestDenyDialogue { get; set; }            // dialogue given when quest requirements are not met
    private string m_QuestGivenDialogue { get; set; }           // dialogue given when quest is already accepted
    private string m_QuestCompletionDialoge { get; set; }       // dialogue given when quest is being completed
    private string m_QuestAfterwordDialogue { get; set; }       // doalogue given after quest is completed (after word)

    public enum QuestState
    {
        Locked,
        Open,
        InProgression,
        Completed
    }

    public enum QuestCompletion
    {
        Exploration,
        ItemRequirement,
        KillRequirement,
        TalkRequirement
    }

    public enum QuestType
    {
        Starter,
        Progresser,
        Completer,
        StartProgression,
        StartComplete,
        ProgresserComplete,
    }

    public QuestInformation()
    {
        m_QuestTitle = "NO TITLE";
        m_QuestID = 0;
    }

    public QuestInformation(int id, string questtitle, int[] questtype, bool queststarter, bool questprogresser, bool questcompleter, int questtotalprogressions, int progresionquestid, int[] questunlocks, bool requiresquestunlock, int[] requiredquestid, bool requireskill, int[] requireskillamount, int[] requiredenemyid, bool requiresitem, int[] requireditemid, string[] questdialogue, string questdeny, string questgiven, string questcomplete, string afterword)
    {
        m_QuestID = id;
        m_QuestTitle = questtitle;
        Debug.Log("ID: " + m_QuestID + " m_QuestTitle: " + m_QuestTitle);
        int QTC = questtype.Length; for (int i = 0; i < QTC; i++) { m_QuestType[QTC] = questtype[QTC]; Debug.Log("Questtype nr." + QTC + " : " +m_QuestType[QTC]); }
        
        m_QuestStarter = queststarter;
        m_QuestProgresser = questprogresser;
        m_QuestCompleter = questcompleter;
        m_QuestProgressionState = 0;
        m_QuestTotalProgressions = questtotalprogressions;
        m_ProgressionQuestID = progresionquestid;
        int QUC = questunlocks.Length; for (int i = 0; i < QUC; i++) { m_QuestUnlocks[QUC] = questunlocks[QUC]; }

        m_RequiresQuestUnlock = requiresquestunlock;
        int RQUC = requiredquestid.Length; for (int i = 0; i < RQUC; i++) { m_RequiredQuestID[RQUC] = requiredquestid[RQUC]; }
        m_RequiresKill = requireskill;
        int RKAC = requireskillamount.Length; for (int i = 0; i < RKAC; i++) { m_RequiresKillAmount[RKAC] = requireskillamount[RKAC]; }
        int REIC = requiredenemyid.Length; for (int i = 0; i < REIC; i++) { m_RequiredEnemyID[REIC] = requiredenemyid[REIC]; }
        m_RequiresItem = requiresitem;
        int RIIC = requireditemid.Length; for (int i = 0; i < RIIC; i++) { m_RequiredItemID[RIIC] = requireditemid[RIIC]; }

        int QDC = questdialogue.Length; for (int i = 0; i < QDC; i++) { m_QuestDialogue[QDC] = questdialogue[QDC]; }
        m_QuestDenyDialogue = questdeny;
        m_QuestGivenDialogue = questgiven;
        m_QuestCompletionDialoge = questcomplete;
        m_QuestAfterwordDialogue = afterword;

        m_QuestStarted = false;
        m_QuestCompleted = false;
        // questcompleted

    }
}
