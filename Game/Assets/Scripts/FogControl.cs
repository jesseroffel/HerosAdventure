using UnityEngine;
using System.Collections;

public class FogControl : MonoBehaviour {
    public bool EnableFog = false;
    private bool currentsettingfog = false;
    private float fogdens = 0.005f;
    private float currentdens = 0;
    private bool fadedone = true;
    // Use this for initialization

    void Start()
    {
        currentsettingfog = RenderSettings.fog;
    }

    void OnTriggerEnter(Collider collision)
    {
        currentsettingfog = RenderSettings.fog;
        if (currentsettingfog != EnableFog && fadedone == true)
        {
            if (collision.isTrigger)
            {
                if (collision.gameObject.tag == "Player")
                {
                    if (EnableFog)
                    {
                        StartCoroutine(FogFadeIn());
                    }
                    else
                    {
                        StartCoroutine(FogFadeOut());

                    }

                }
            }
        }

    }

    IEnumerator FogFadeIn()
    {
        fadedone = false;
        RenderSettings.fog = true;
        for (float i = 0; i < 5;i++)
        {
            currentdens += 0.001f;
            RenderSettings.fogDensity = currentdens;
            yield return new WaitForSeconds(0.1f);
        }
        fadedone = true;
    }

    IEnumerator FogFadeOut()
    {
        fadedone = false;
        currentdens = fogdens;
        for (float i = 0; i < 5; i++)
        {
            currentdens -= 0.001f;
            RenderSettings.fogDensity = currentdens;
            yield return new WaitForSeconds(0.1f);
        }
        RenderSettings.fog = false;
        fadedone = true;
    }
}
