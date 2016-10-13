using UnityEngine;
using System.Collections;

public class NPCObject : MonoBehaviour {
    public int m_NPCID { get; set; }
    public bool m_Sex { get; set; }
    public bool m_Interactable { get; set; }
    public string[] m_Dialogue { get; set; }
    public int m_QuestID { get; set; }
    public string m_Name { get; set; }

    public NPCObject(int npcid, string name, bool sex, bool interactable, string[] dialogue)
    {
        m_NPCID = npcid;
        m_Name = name;
        m_Sex = sex;
        m_Interactable = interactable;
        m_Dialogue = dialogue;
        m_QuestID = 0;
    }

    public NPCObject(int npcid, string name, bool sex, bool interactable, int questid)
    {
        m_NPCID = npcid;
        m_Name = name;
        m_Sex = sex;
        m_Interactable = interactable;
        m_QuestID = questid;
    }

}
