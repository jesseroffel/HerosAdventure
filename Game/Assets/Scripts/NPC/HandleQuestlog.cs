using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleQuestlog : MonoBehaviour {
    public GameObject QuestList;
    public GameObject QuestItemPrefab;
    public GameObject ObjectiveReqPrefab;
    public GameObject ObjectiveInfoPrefab;

    public GameObject QuestUnlockWindow;


    void Start () {
        if (QuestList == null) { Debug.LogError("GameObject QuestList no has reference!"); }
        if (QuestItemPrefab == null) { Debug.LogError("GameObject QuestItemPrefab no has reference!"); }
        if (ObjectiveReqPrefab == null) { Debug.LogError("GameObject ObjectiveReqPrefab no has reference!"); }
        if (ObjectiveInfoPrefab == null) { Debug.LogError("GameObject ObjectiveInfoPrefab no has reference!"); }

        if (QuestUnlockWindow == null) { Debug.LogError("GameObject QuestUnlockWindow no has reference!"); }
    }
	

	//void Update () {
	
	//}

    public void AddQuestToList(int questid, string title)
    {
        /*GameObject Quest = Instantiate(QuestItemPrefab);
        Quest.transform.SetParent(QuestList.transform);
        Quest.name = questid + "";
        Vector3 a = new Vector3(1, 1, 1);
        Quest.transform.localScale = a;
        HandleQLDetails Details = Quest.GetComponent<HandleQLDetails>();
        Details.SetTitle(title);
        Debug.Log("[QUESTLOG] Added Quest [" + title + "] to the Questlog");
        */
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

    public void DisplayQuestUnlock(string questtitle)
    {
        GameObject QuestDisplay = Instantiate(QuestUnlockWindow);
        QuestDisplay.transform.SetParent(transform);

        Vector3 a = new Vector3(0.3f, 0.2f, 1);
        QuestDisplay.transform.localScale = a;

        //RectTransform RT = QuestDisplay.GetComponent<RectTransform>();
        //RT.

        Transform Title = QuestDisplay.transform.GetChild(1);
        Title.GetComponent<Text>().text = questtitle;

        Debug.Log("[QUESTLOG] Displaying Unlock [" + questtitle + "]");
    }
}
