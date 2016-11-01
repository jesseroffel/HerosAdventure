using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleQLObjective : MonoBehaviour {
    public Text AmountText;
    public Text ObjectiveText;

    int totalamount = 0;

	// Use this for initialization
	void Start () {
        if (AmountText == null) { Debug.LogError("Text AmountText has no reference!"); }
        if (ObjectiveText == null) { Debug.LogError("Text ObjectiveText has no reference!"); }
    }
	
    public void SetAmount(int currentamount, int endamount)
    {
        AmountText.text = currentamount + " / " + endamount;
        totalamount = endamount;
    }

    public void UpdateAmount(int currentamount)
    {
        AmountText.text = currentamount + " / " + totalamount;
    }

    public void SetObjective(string objective)
    {
        ObjectiveText.text = objective;
    }
}
