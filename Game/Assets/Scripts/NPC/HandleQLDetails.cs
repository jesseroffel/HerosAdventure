using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleQLDetails : MonoBehaviour {
    public Text TitleObject;

	void Start () {
	    if (TitleObject == null) { Debug.LogError("Text TitleObject has no reference!"); }
	}


    public void SetTitle(string title)
    {
        TitleObject.text = title;
    }
}
