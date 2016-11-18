using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleQuestlog : MonoBehaviour {
    [Header("Quest UI Panel")]
    public GameObject QuestLogPanel;
    [Header("Quest List Grid Prefab")]
    public GameObject QuestListGrid;
    [Header("Quest Item Prefab")]
    public GameObject QuestItemPrefab;
    [Header("Quest Item Requirement Prefabs")]
    public GameObject ObjectiveReqPrefab;
    public GameObject ObjectiveInfoPrefab;
    [Header("Quest Unlock Window")]
    public GameObject QuestUnlockPanel;
    public GameObject QuestUnlockPrefab;

    private bool Active = false;
    private float CheckActive = 0.0f;
    Vector3 DefaultScale = new Vector3(1f, 1f, 1);
    void Start () {
        if (QuestLogPanel == null) { Debug.LogError("GameObject QuestLogPanel no has reference!"); }
        if (QuestListGrid == null) { Debug.LogError("GameObject QuestListGrid no has reference!"); }
        if (QuestItemPrefab == null) { Debug.LogError("GameObject QuestItemPrefab no has reference!"); }
        if (ObjectiveReqPrefab == null) { Debug.LogError("GameObject ObjectiveReqPrefab no has reference!"); }
        if (ObjectiveInfoPrefab == null) { Debug.LogError("GameObject ObjectiveInfoPrefab no has reference!"); }

        if (QuestUnlockPanel == null) { Debug.LogError("GameObject QuestUnlockPanel no has reference!"); }
        if (QuestUnlockPrefab == null) { Debug.LogError("GameObject QuestUnlockWindow no has reference!"); }
    }
	

	void Update () {
	    if (Time.time > CheckActive)
        {
            CheckActive = Time.time + 1.0f;
            CheckList();
        }
	}

    public void AddQuestToList(int questid, string title, string dialogue, int[] regitemid, int[] regenemyid, int[] regkillamount, int[] currentitems, int[] currentkills)
    {
        GameObject Quest = Instantiate(QuestItemPrefab);
        Quest.transform.SetParent(QuestListGrid.transform);
        Quest.name = questid + "";
        Quest.transform.localScale = DefaultScale;

        HandleQLDetails Details = Quest.GetComponent<HandleQLDetails>();
        Details.SetTitle(title);
        int ridl = regitemid.Length;
        int reil = regenemyid.Length;
        int rkal = regkillamount.Length;
        // ITEM ID
        for (int i = 0; i < ridl; i++) {
            int id = regitemid[i];
            GameObject QRP = Instantiate(ObjectiveReqPrefab);
            QRP.transform.SetParent(Quest.transform.GetChild(1));
            QRP.transform.localScale = DefaultScale;
            HandleQLObjective objectivedetails = QRP.GetComponent<HandleQLObjective>();
            objectivedetails.SetAmount(currentitems[i], 1);
            ItemDatabase ID = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemDatabase>();
            string ItemName = ID.FetchItemNameByID(id);
            objectivedetails.SetObjective(ItemName + " found");
        }

        // ENEMIES
        for (int i = 0; i < reil; i++)
        {
            int eid = regenemyid[i];
            int rka = regkillamount[i];
            GameObject QRP = Instantiate(ObjectiveReqPrefab);
            QRP.transform.parent = Quest.transform.GetChild(1);
            QRP.transform.localScale = DefaultScale;
            HandleQLObjective objectivedetails = QRP.GetComponent<HandleQLObjective>();
            objectivedetails.SetAmount(currentkills[0], rka);
            // GET INFO FROM A ENEMY LISY OMG
            objectivedetails.SetObjective(" Slimes killed");
        }

            Debug.Log("[QUESTLOG] Added Quest [" + title + "] to the Questlog");
        
    }

    public void UpdateLog(int questid, int[] currentitems, int[] currentkills)
    {
        int childs = QuestListGrid.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            Transform currentquest = QuestListGrid.transform.GetChild(i);
            if (currentquest.name == questid.ToString())
            {
                int curitemcount = currentitems.Length;
                int curkillamount = currentkills.Length;
                Transform Requirements = currentquest.GetChild(1);
                int reqchildcount = Requirements.childCount;
                for (int rcc = 0; rcc < reqchildcount; rcc++)
                {
                    for (int ic = 0; ic < curitemcount; ic++)
                    {
                        HandleQLObjective HQLD = Requirements.GetChild(rcc).GetComponent<HandleQLObjective>();
                        HQLD.UpdateAmount(currentitems[i]);
                    }
                    for (int ik = 0; ik < curkillamount; ik++)
                    {
                        HandleQLObjective HQLD = Requirements.GetChild(rcc).GetComponent<HandleQLObjective>();
                        HQLD.UpdateAmount(currentkills[i]);
                    }
                }
                

                
            }
        }
    }

    void CheckList()
    {
        if (Active)
        {
            if (QuestListGrid.transform.childCount == 0)
            {
                QuestLogPanel.SetActive(false);
                Active = false;
            }
        } else
        {
            if (QuestListGrid.transform.childCount > 0)
            {
                Active = true;
                QuestLogPanel.SetActive(true);
            }
        }
        
    }

    public void RemoveQuestFromList(int questid)
    {
        int childcount = QuestListGrid.transform.childCount;
        for (int i = 0; i < childcount; i++)
        {
            GameObject obj = QuestListGrid.transform.GetChild(i).gameObject;
            if (obj.name == questid.ToString())
            {
                Debug.Log("[QUESTLOG] Removed Quest " + questid + " from the Questlog");
                Destroy(obj);
                break;
            }
        }
        
    }

    public void DisplayQuestUnlock(string questtitle)
    {
        GameObject QuestDisplay = Instantiate(QuestUnlockPrefab);
        QuestDisplay.transform.SetParent(QuestUnlockPanel.transform);
        QuestDisplay.transform.localScale = DefaultScale;
        QuestDisplay.transform.position = new Vector3(0,0,0);


        //RectTransform RT = QuestDisplay.GetComponent<RectTransform>();
        /*Left*/
        //RT.offsetMin.Set(1,1);
        /*Right*/
        //RT.offsetMax.x = reset;
        /*Top*/
        //RT.offsetMax.Set(1, 1);
        /*Bottom*/
        //RT.offsetMin.y;

        Transform Title = QuestDisplay.transform.GetChild(1);
        Title.GetComponent<Text>().text = questtitle;

        Debug.Log("[QUESTLOG] Displaying Unlock [" + questtitle + "]");
    }
}
