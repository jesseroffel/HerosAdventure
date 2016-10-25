using UnityEngine;
using System.Collections;

public class PlayerIcon : MonoBehaviour {
    private bool IconMoveUp = true;
    private RectTransform RectT;
    private Camera cam;

    
    void Start()
    {
        RectT = gameObject.GetComponent<RectTransform>();
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
    }
    
	void Update () {
        LookAtCamera();
        HoverIcon();
    }

    void LookAtCamera()
    {
        RectT.LookAt(cam.transform.position);
        RectT.Rotate(new Vector3(0, 180, 0));
    }

    void HoverIcon()
    {
        if (RectT)
        {
            if (IconMoveUp)
            {
                if (RectT.localPosition.y < 5) { RectT.localPosition += new Vector3(0, 0.1f, 0); } else { IconMoveUp = false; }
            }
            else
            {
                if (RectT.localPosition.y > 0) { RectT.localPosition -= new Vector3(0, 0.1f, 0); } else { IconMoveUp = true; }
            }
        }
    }
}
