using UnityEngine;
using System.Collections;

public class NPCObject {
    public int m_NPCID { get; set; }
    public bool m_Sex { get; set; }
    public bool m_Interactable { get; set; }
    public string[] m_Dialogue { get; set; }
    public int m_QuestID { get; set; }
    public string m_Name { get; set; }
    public float  m_TalkSpeed { get; set; }

    public NPCObject(int npcid, string name, bool sex, bool interactable, float talkspeed, int questid, string[] dialogue)
    {
        m_NPCID = npcid;
        m_Name = name;
        m_Sex = sex;
        m_Interactable = interactable;
        m_TalkSpeed = talkspeed;
        m_Dialogue = dialogue;
        m_QuestID = questid;
    }
}
