using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private string title;
    private string description;
    private string value;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {

    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        title = item.Title;
        description = item.Description;

        tooltip.transform.GetChild(0).GetComponent<Text>().text = title;
        tooltip.transform.GetChild(1).GetComponent<Text>().text = description;
    }

}
