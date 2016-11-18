using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCList : MonoBehaviour {
    public static NPCList NPCListObject;
    [Header("NPC List Stats")]
    public int NPCCount = 0;
    public int LastLoadedID = 0;
    private List<NPCObject> DatabaseNPCs = new List<NPCObject>();

	// Use this for initialization
	void Start () {
        if (NPCListObject == null)
        {
            NPCListObject = this;
        }
        else if (NPCListObject != this)
        {
            Destroy(gameObject);
        }

        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Norwood",                // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.05f,                              // Talkspeed between characters
            0,
            new string[] { "The situation is a mess..", "Oh, excuse me, I was talking to myself there..", "I haven't seen you around before? Are you wandering around?", "Well I better warn you, there is much going on here, dangerous stuff I tell you!", "Please speak to Elliot Cross, he is our mayor, he will explain the situation.."  } // NO QUEST? ADD DIALOGUE
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Elliot Cross",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.04f,                         // Talkspeed between characters
            0,                               // QUEST ID
            new string[] { "Ah! You look like someone that could help us right now.", "There is a creature that haunts our little island, everyday I hear that people go missing.", "From what I've heard the creature uses dark magic and hides in that big tower in the distance", "I think you should explore the world first before you attempt killing that creature","Please speak to my collegues next to me, they will help you out" }
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Flint Ironhand",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.05f,                            // Talkspeed between characters
            1,                               // QUEST ID
            new string[] { }
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Thadeus",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.05f,                            // Talkspeed between characters
            2,                               // QUEST ID
            new string[] { }
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Griff",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.03f,                            // Talkspeed between characters
            0,                               // QUEST ID
            new string[] { "Watch your health boy, don't forget to use your items to heal when in danger!", "I sense you also use some kind of magic, use that to get some health when you got plenty energy."}
        ));
        DatabaseNPCs.Add(new NPCObject(
            DatabaseNPCs.Count + 1,         // ID
            "Suman",                    // NPC Name
            true,                           // SEX
            true,                           // INTERACTABLE
            0.03f,                            // Talkspeed between characters
            0,                               // QUEST ID
            new string[] { "You look like you use multiple combat styles, I got some tips for you", "Switching to another combatstyle can be handy in battle, some are more useful " }
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
