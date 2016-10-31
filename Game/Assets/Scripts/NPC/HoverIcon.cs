using UnityEngine;
using System.Collections;

public class HoverIcon : MonoBehaviour {
    private bool IconMoveUp = true;
    private Camera cam;
    private float startpos;

    
    void Start()
    {
        startpos = transform.position.y;
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
    }
    
	void Update () {
        if (cam)
        {
            LookAtCamera();
            Hover();
        }
    }

    void LookAtCamera()
    {
        transform.LookAt(cam.transform.position);
        //transform.Rotate(new Vector3(0, 180, 0));
    }

    void Hover()
    {
        if (transform)
        {
            if (IconMoveUp)
            {
                if (transform.localPosition.y < startpos + 0.15f) { transform.localPosition += new Vector3(0, 0.0025f, 0); } else { IconMoveUp = false; }
            }
            else
            {
                if (transform.localPosition.y > startpos) { transform.localPosition -= new Vector3(0, 0.0025f, 0); } else { IconMoveUp = true; }
            }
        }
    }
}
