using LitJson;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class QuestList : MonoBehaviour {
    public string QuestJSONPath = Application.dataPath + "/StreamingAssets/Quests.json";
    private int QuestID = 0;

    private List<QuestInformation> DatabaseQuests = new List<QuestInformation>();
    private JsonData QuestData;

    


    // Use this for initialization
    void Start () {
        QuestData = JsonMapper.ToObject(File.ReadAllText(QuestJSONPath));

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ConstructQuestDatabase()
    {
        for (int i = 0; i < QuestData.Count; i++)
        {
            //DatabaseQuests.Add(new QuestInformation(
            //    QuestData[i][""],

            //);
        }
    }
}
