using UnityEngine;
using System.Collections;

public class gameoverscene : MonoBehaviour {

    ChangeScene scene;


	void Start ()
    {
        scene = GameObject.Find("GameHandler").GetComponent<ChangeScene>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            scene.ChangeToScene(0);
        }     
	}
}
