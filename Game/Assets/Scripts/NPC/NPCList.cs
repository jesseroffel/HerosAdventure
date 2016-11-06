using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCList : MonoBehaviour {
    public static NPCList NPCListObject;
    public int NPCCount = 0;
    public int LastLoadedID = 0;
    private List<NPCObject> DatabaseNPCs = new List<NPCObject>();

	// Use this for initialization
	void Start () {
        if (NPCListObject == null)
        {
            NPCListObject = this;
            Debug.Log("Set NPC LIST");
        }
        else if (NPCListObject != this)
        {
            Destroy(gameObject);
        }

        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Sir Testalot",                // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.05f,                              // Talkspeed between characters
            0,
            new string[] { "This is a test line", "This is the second one", "goodbye" } // NO QUEST? ADD DIALOGUE
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Old Man",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.00125f,                         // Talkspeed between characters
            1,                               // QUEST ID
            new string[] { }
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Slime Hater",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.085f,                            // Talkspeed between characters
            2,                               // QUEST ID
            new string[] { }
        ));
        NPCCount = DatabaseNPCs.Count;
    }
    
    public NPCObject GetInformation(int npcid)
    {

        foreach (NPCObject npc in DatabaseNPCs)
        {
            if (npc.m_NPCID == npcid)
            {
                LastLoadedID = npcid;
                return npc;
            }
        }
        NPCObject obj = null;
        return obj;
    }

    
}
