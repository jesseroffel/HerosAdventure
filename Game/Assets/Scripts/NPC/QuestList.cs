using LitJson;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class QuestList : MonoBehaviour {
    public int QuestCount = 0;
    private List<QuestInformation> DatabaseQuests = new List<QuestInformation>();
    void Start () {

        //QuestData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Quests.json"));
        //ConstructQuestDatabase();
        DatabaseQuests.Add(new QuestInformation(
            DatabaseQuests.Count+1,
            "Test quest",
            new int[] { 0, 2 },
            true,
            false,
            true,
            1,
            0,
            new int[] { 0, 2 },
            false,
            new int[] { },
            false,
            new int[] { },
            new int[] { },
            false,
            new int[] { },
            new string[] { "AAAA", "BBBB", "CCCC!" },
            "Deny",
            "given",
            "complete",
            "Afterword"
        ));
        QuestCount = DatabaseQuests.Count;
    }

    void Update()
    {
        //Debug.Log(DatabaseQuests);
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
