using UnityEngine;
using System.Collections;

public class MenuNewPanel : MonoBehaviour
{
    public GameObject NewPanel;
    public GameObject ThisPanel;

    void Start()
    {
        //ThisPanel = gameObject.transform.parent;
    }

    public void GoToNewPanel()
    {
        if (NewPanel)
        {
            NewPanel.SetActive(true);
            ThisPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("No reference found: NewPanel");
        }
    }
}
