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
            0.5f,
            0,
            1,
            "MagicMissle.png"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Slowing Down",
            50,
            0,
            0.25f,
            5,
            2,
            "aoeslowness.png"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Poisoning",
            100,
            0,
            0.25f,
            2.5f,
            3,
            "aoepoison.png"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            2,
            "Circle of Fire",
            100,
            0,
            0.25f,
            2.5f,
            4,
            "aoefire.png"
        ));
        SpellList.Add(new Spell(
            SpellList.Count + 1,
            3,
            "Quick Patchup",
            100,
            50,
            0,
            0,
            0,
            "heal.png"
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