using LitJson;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class QuestList : MonoBehaviour {
    private int QuestID = 0;

    private List<QuestInformation> DatabaseQuests = new List<QuestInformation>();
    private JsonData QuestData;

    


    // Use this for initialization
    void Start () {

        QuestData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Quests.json"));
        Debug.Log("'a'");
        ConstructQuestDatabase();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ConstructQuestDatabase()
    {
        for (int i = 0; i < QuestData.Count; i++)
        {
            List<int> questtypeslist = new List<int>();
            questtypeslist.Add((int)QuestData[i]["questtype"]["type"]);
            Debug.Log("B");

            /*DatabaseQuests.Add(new QuestInformation(
                (int)QuestData[i]["id"],
                (string)QuestData[i]["title"],
                (int)QuestData[i]["questtype"]["type"],
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
            */
        }
    }
}
