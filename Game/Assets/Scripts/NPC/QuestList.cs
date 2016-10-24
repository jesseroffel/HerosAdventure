using LitJson;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class QuestList : MonoBehaviour {
    public int QuestCount = 0;
    public int ActiveQuestCount = 0;
    public int LastAccessedQuest = 0;
    private List<QuestObject> DatabaseQuests = new List<QuestObject>();
    private List<QuestObject> ActiveQuests = new List<QuestObject>();

    void Start () {

        //QuestData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Quests.json"));
        //ConstructQuestDatabase();
        DatabaseQuests.Add(new QuestObject(
            DatabaseQuests.Count+1,             // QuestID
            "Test quest",                       // Quest Title
            new int[] { 0, 2 },                 // Quest Type(s)
            true,                               // Quest Starter?
            false,                              // Quest Progresser?
            true,                               // Quest Completer?
            1,                                  // Quest Progression Start State
            0,                                  // Quest Total Progressions
            new int[] { 0, 2 },                 // Other Quest ID unlock(s) at completion, leave empty if none
            false,                              // Requires Quest ID to start?
            new int[] { },                      // Required Quest ID(s) to start, leave empty if none
            false,                              // Requires Kill to complete?
            new int[] { },                      // Required Enemy kill amount to complete, leave empty if none
            new int[] { },                      // Required EnemyID(s) for kill tracking, leave empty if none
            false,                              // Requires item to complete?
            new int[] { },                      // Required item id(s) to complete, leave empty if none
            new string[] { "AAAA", "BBBB", "CCCC!" },      // Dialogue strings about quest
            "Deny",                             // Dialogue when start conditions are not met
            "given",                            // Dialogue when quest is already started
            "complete",                         // Dialogue when completing quest
            "Afterword"                         // Dialogue after completion quest
        ));
        QuestCount = DatabaseQuests.Count;
    }

    public bool CheckCompletedQuest(int questid)
    {
        foreach (QuestObject activequest in ActiveQuests)
        {
            if (activequest.m_QuestID == questid)
            {
                if (activequest.m_QuestCompleted) { return true; } else { return false; }
            }
        }
        return false;
    }

    public QuestObject GetInformation(int questid)
    {
    
        foreach (QuestObject quest in DatabaseQuests)
        {
           if (quest.m_QuestID == questid)
            {
                LastAccessedQuest = questid;
                return quest;
            }
        }
        QuestObject obj = null;
        return obj;
    }

    public bool AddActiveQuest(int questid)
    {
        bool alreadyadded = false;
        foreach (QuestObject activequest in ActiveQuests)
        {
            if (activequest.m_QuestID == questid)
            {
                Debug.LogWarning("[QUEST] Quest Already added!?");
                LastAccessedQuest = questid;
                return true;
            }
        }
        if (!alreadyadded)
        {
            foreach (QuestObject quest in DatabaseQuests)
            {
                if (quest.m_QuestID == questid)
                {
                    ActiveQuests.Add(quest);
                    LastAccessedQuest = questid;
                    ActiveQuestCount = ActiveQuests.Count;
                    Debug.Log("[QUEST] Set Active Quest: " + quest.m_QuestID);
                    return true;
                }
            }
            
        }
        return false;
    }

    public bool FinishQuest(int questid)
    {
        foreach (QuestObject activequest in ActiveQuests)
        {
            if (activequest.m_QuestID == questid)
            {
                activequest.m_QuestCompleted = true;
                return true;
            }
        }
        return false;
    }

    public bool StartQuest(int questid)
    {
        foreach (QuestObject activequest in ActiveQuests)
        {
            if (activequest.m_QuestID == questid)
            {
                activequest.m_QuestStarted = true;
                return true;
            }
        }
        return false;
    }

    /*void ConstructQuestDatabase()
    {
        for (int i = 0; i < QuestData.Count; i++)
        {
            QuestData[i]["questtypes"]
            List<int> questtypeslist = new List<int>();
            //questtypeslist.AddRange((int)QuestData[i]["questtypes"]);
            
            DatabaseQuests.Add(new QuestInformation(
                (int)QuestData[i]["id"],
                (string)QuestData[i]["title"],
                (int)QuestData[i]["questtype"],
                (bool)QuestData[i]["queststarter"],
                (bool)QuestData[i]["questprogresser"],
                (bool)QuestData[i]["questcompleter"],
                (int)QuestData[i]["questtotalprogressions"],
                (int)QuestData[i]["progresionquestid"],
                (int)QuestData[i]["questunlocks"],
                (bool)QuestData[i]["requiresquestunlock"],
                (int)QuestData[i]["requiredquestid"],
                (bool)QuestData[i]["requireskill"],
                (int)QuestData[i]["requireskillamount"],
                (int)QuestData[i]["requiredenemyid"],
                (bool)QuestData[i]["requiresitem"],
                (int)QuestData[i]["requireditemid"],
                (string)QuestData[i]["questdialogue"],
                (string)QuestData[i]["questdeny"],
                (string)QuestData[i]["questgiven"],
                (string)QuestData[i]["questcomplete"],
                (string)QuestData[i]["afterword"]

                ));
            
    }*/
}
