using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (id == database[i].ID)
            {
                return database[i];
            }
        }
        return null;
    }

    public string FetchItemNameByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (id == database[i].ID)
            {
                return database[i].Title;
            }
        }
        return "";
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"],
                (string)itemData[i]["title"],
                (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["attack"],
                (int)itemData[i]["stats"]["defence"],
                (int)itemData[i]["stats"]["power"],
                (string)itemData[i]["description"],
                (bool)itemData[i]["stackable"],
                (bool)itemData[i]["consumable"],
                (string)itemData[i]["slug"]
                ));
        }
    }
}


public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Power { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public bool Consumable { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int value, int attack, int defence, int power, string description, bool stackable, bool consumable, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Attack = attack;
        this.Defence = defence;
        this.Power = power;
        this.Description = description;
        this.Stackable = stackable;
        this.Consumable = consumable;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item()
    {
        this.ID = -1;
    }
}