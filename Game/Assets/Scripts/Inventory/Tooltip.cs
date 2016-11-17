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

    public void Activate(Item item)
    {
        this.item = item;
        ConstructTooltip();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructTooltip()
    {
        title = item.Title;
        description = item.Description;

        tooltip.transform.GetChild(0).GetComponent<Text>().text = title;
        tooltip.transform.GetChild(1).GetComponent<Text>().text = description;
    }

}
