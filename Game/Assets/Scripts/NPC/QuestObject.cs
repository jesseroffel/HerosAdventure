using UnityEngine;
using System.Collections;

public class QuestObject  {

    public int m_QuestID { get; set; }
    public string m_QuestTitle { get; set; }

    private int[] m_QuestType { get; set; }                     // Quest type (multiple allowed) 
    private bool m_QuestStarter { get; set; }                   // if true this quest will start the questline or just itself
    private bool m_QuestProgresser { get; set; }                // if true this quest will progress into another quest
    private bool m_QuestCompleter { get; set; }                 // if true this quest will complete the questline or just itself

    public bool m_QuestStarted { get; set; }                   // Quest started or not
    public bool m_QuestCompleted { get; set; }                 // Quest complete or not
    private int m_QuestProgressionState { get; set; }           // current state
    private int m_QuestTotalProgressions { get; set; }          // total stats of quest                    // Current state of quest
    private int m_ProgressionQuestID { get; set; }              // the quest will transform to this ID as followup
    private int[] m_QuestUnlocks { get; set; }                  // Completing this quest will unlock quest(s)   (multiple allowed) 

    public bool m_RequiresQuestUnlock { get; set; }            // This quest wont start without quest(s) completed if true
    public int[] m_RequiredQuestID { get; set; }               // this quest(s) need to be completed to start this one (multiple allowed) 

    private bool m_RequiresKill { get; set; }
    private int[] m_RequiresKillAmount { get; set; }
    private int[] m_RequiredEnemyID { get; set; }

    private bool m_RequiresItem { get; set; }
    private int[] m_RequiredItemID { get; set; }

    public bool m_QuestRequirementsMet { get; set; }

    public string[] m_QuestDialogue { get; set; }              // dialogue given when taking about the quest       (multiple allowed) 
    public string m_QuestDenyDialogue { get; set; }            // dialogue given when quest requirements are not met
    public string m_QuestGivenDialogue { get; set; }           // dialogue given when quest is already accepted
    public string m_QuestCompletionDialoge { get; set; }       // dialogue given when quest is being completed
    public string m_QuestAfterwordDialogue { get; set; }       // doalogue given after quest is completed (after word)

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

    public QuestObject()
    {
        m_QuestTitle = "NO TITLE";
        m_QuestID = 0;
    }

    public QuestObject(int id, string questtitle, int[] questtype, bool queststarter, bool questprogresser, bool questcompleter, int questtotalprogressions, int progresionquestid, int[] questunlocks, bool requiresquestunlock, int[] requiredquestid, bool requireskill, int[] requireskillamount, int[] requiredenemyid, bool requiresitem, int[] requireditemid, string[] questdialogue, string questdeny, string questgiven, string questcomplete, string afterword)
    {
        m_QuestID = id;
        m_QuestTitle = questtitle;

        int QTC = questtype.Length;
        m_QuestType = new int[QTC];
        for (int i = 0; i < QTC; i++) { m_QuestType[i] = questtype[i]; }
        
        m_QuestStarter = queststarter;
        m_QuestProgresser = questprogresser;
        m_QuestCompleter = questcompleter;
        m_QuestProgressionState = 0;
        m_QuestTotalProgressions = questtotalprogressions;
        m_ProgressionQuestID = progresionquestid;

        int QUC = questunlocks.Length;
        m_QuestUnlocks = new int[QUC];
        for (int i = 0; i < QUC; i++) { m_QuestUnlocks[i] = questunlocks[i]; }

        m_RequiresQuestUnlock = requiresquestunlock;

        int RQUC = requiredquestid.Length;
        m_RequiredQuestID = new int[RQUC];
        for (int i = 0; i < RQUC; i++) { m_RequiredQuestID[i] = requiredquestid[i]; }

        m_RequiresKill = requireskill;
        int RKAC = requireskillamount.Length;
        m_RequiresKillAmount = new int[RKAC];
        for (int i = 0; i < RKAC; i++) { m_RequiresKillAmount[i] = requireskillamount[i]; }

        int REIC = requiredenemyid.Length;
        m_RequiredEnemyID = new int[REIC];
        for (int i = 0; i < REIC; i++) { m_RequiredEnemyID[i] = requiredenemyid[i]; }

        m_RequiresItem = requiresitem;

        int RIIC = requireditemid.Length;
        m_RequiredItemID = new int[RIIC];
        for (int i = 0; i < RIIC; i++) { m_RequiredItemID[i] = requireditemid[i]; }

        int QDC = questdialogue.Length;
        m_QuestDialogue = new string[QDC];
        for (int i = 0; i < QDC; i++) { m_QuestDialogue[i] = questdialogue[i]; }

        m_QuestDenyDialogue = questdeny;
        m_QuestGivenDialogue = questgiven;
        m_QuestCompletionDialoge = questcomplete;
        m_QuestAfterwordDialogue = afterword;

        m_QuestStarted = false;
        m_QuestCompleted = false;
        m_QuestRequirementsMet = false;
        // questcompleted

    }
}
