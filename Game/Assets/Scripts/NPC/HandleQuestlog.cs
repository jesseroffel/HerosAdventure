using UnityEngine;
using System.Collections;

public class HandleQuestlog : MonoBehaviour {
    public GameObject QuestList;
    public GameObject QuestPrefab;


	void Start () {
        if (QuestList == null) { Debug.LogError("GameObject QuestList no has reference!"); }
    }
	

	//void Update () {
	
	//}

    public void AddQuestToList(int questid, string title)
    {
        GameObject Quest = Instantiate(QuestPrefab);
        Quest.transform.SetParent(QuestList.transform);
        Quest.name = questid + "";
        Vector3 a = new Vector3(1, 1, 1);
        Quest.transform.localScale = a;
        HandleQLDetails Details = Quest.GetComponent<HandleQLDetails>();
        Details.SetTitle(title);
        Debug.Log("[QUESTLOG] Added Quest " + title + " to the Questlog");
    }

    public void RemoveQuestFromList(int questid)
    {
        /*
        foreach (GameObject child in QuestList)
        {
            if (child.name == questid.ToString())
            {
                Debug.Log("[QUESTLOG] Removed Quest " + questid + " from the Questlog");
                child.SetActive(false);
                break;
            }
        }
        */
    }
}
