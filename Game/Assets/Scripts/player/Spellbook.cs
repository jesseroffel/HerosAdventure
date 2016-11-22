using UnityEngine;
using System.Collections.Generic;

public class Spellbook : MonoBehaviour {
    public static Spellbook SpellbookObject;
    public int AmountSpells = 0;
    private List<Spell> SpellList = new List<Spell>();
	
    void Start()
    {
        if (SpellbookObject == null)
        {
            //DontDestroyOnLoad(gameObject);
            SpellbookObject = this;
        }
        else if (SpellbookObject != this)
        {
            Destroy(gameObject);
        }

        SpellList.Add(new Spell(
            SpellList.Count + 1,
            1,
            "Magic Missle",
            10,
            25f,
            0.15f,
            5,
            1,
            "MagicMissle"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Slowing Down",
            20,
            0,
            0.75f,
            5,
            2,
            "aoeslowness"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Poisoning",
            45,
            0,
            0.5f,
            3.0f,
            3,
            "aoepoison"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Fire",
            35,
            0,
            0.5f,
            2.5f,
            4,
            "aoefire"
        ));

        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Bound to the ground",
            65,
            50,
            0.5f,
            2,
            6,
            "bind"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            3,
            "Quick Patchup",
            50,
            50,
            0,
            0,
            0,
            "heal"
        ));

        

        AmountSpells = SpellList.Count;
    }

    public Spell GetSpellByID(int id)
    {
        foreach (Spell spell in SpellList)
        {
            if (spell.ID == id)
            {
                return spell;
            }
        }

        Spell emptyspell = new Spell();
        return emptyspell;
    }

}


public class Spell
{
    public int ID { get; set; }
    public int SpellType { get; set; }
    public string SpellName { get; set; }
    public int ManaCost { get; set; }
    public float Change { get; set; }
    public float CastTime { get; set; }
    public float AliveTime { get; set; }
    public int Appearence { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Spell(int id, int spelltype, string spellname, int manacost, float change, float casttime, float alivetime, int appearence, string slug)
    {
        this.ID = id;
        this.SpellType = spelltype;
        this.SpellName = spellname;
        this.ManaCost = manacost;
        this.Change = change;
        this.CastTime = casttime;
        this.AliveTime = alivetime;
        this.Appearence = appearence;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Spells/" + slug);
    }

    public Spell()
    {
        this.ID = -1;
    }
}