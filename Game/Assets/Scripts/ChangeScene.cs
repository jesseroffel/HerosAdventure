using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour 
{
	public static int scene;
    private static ChangeScene instance;

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!instance)
        {
            instance = this;
        }           
        //otherwise, if we do, kill this thing
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeToScene (int ChangeSceneTo)
	{
		scene = ChangeSceneTo;
		if (ChangeSceneTo == 9)
		{
			Debug.Log("exit");
			Application.Quit();
		}
		Application.LoadLevel(ChangeSceneTo);
	}		


}
