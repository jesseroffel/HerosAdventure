using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleULWindow : MonoBehaviour {
    //public Image QWImage;
    //public Text QUText;
    //public Text QuestTitle;

    //int currentval = 0;
    //Color CurColor = new Vector4(0, 0, 0, 0);
    //bool fadein = true;
    float CurTime = 0;
    //bool active = true;


    void Start () {
        CurTime = Time.time + 5.0f;
    }
	

	void Update () {
        //if (active)
        //{
        //    Check();
        //} else
        //{
        //    Destroy(gameObject);
        //}
        WaitForDelete();

    }

    void WaitForDelete()
    {
        if (CurTime < Time.time) { Destroy(gameObject); }
    }
    /*
     
    void Check()
    {
        if (fadein) { FadeIn(); } else { FadeOut(); }
    }



    void FadeOut()
    {
        if (CurTime < Time.time)
        {
            CurTime += Time.time + 0.1f;
            if (currentval > 0)
            {
                //QWImage.color += Vector4(0, 0, 0, 0.1);
                //QUText.color = CurColor;
                //QuestTitle.color = CurColor;
                //currentval--;
                CurColor = new Vector4(currentval, currentval, currentval, currentval);
            }
            if (currentval == 0)
            {
                active = false;
            }
        }
    }

    void FadeIn()
    {
        if (CurTime < Time.time)
        {
            CurTime += Time.time + 0.1f;
            if (currentval < 255)
            {
                
                QWImage.color = CurColor;
                QUText.color = CurColor;
                QuestTitle.color = CurColor;
                currentval++;
                CurColor = new Vector4(currentval, currentval, currentval, currentval);
            }
            if (currentval == 255)
            {
                fadein = false;
            }
        }
    }
    */
}
