using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Menu : MonoBehaviour {
    public Image Fadescreen;
    public GameObject MenuScreen;
    public GameObject Titlecredits;
    public GameObject CameraObject;
    public GameObject[] Cameras;
    public AudioSource AudioSource;
    public int GameSceneIndex = 1;

    private bool IntroEnded = false;
    private bool WaitingForInput = false;
    private bool InMenu = false;
    private bool FadeOut = false;
    private bool FirstFade = true;
    private float Fade = 1;
    private float MusicFade = 0;
    private int CameraIndex = 0;
    private int MaxCams = 0;
    private bool GetCamPos = true;
    private bool GetCamera = true;
    private Vector3 OldCamPos;

	// Use this for initialization
	void Start () {
        MaxCams = Cameras.Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingForInput && IntroEnded == true)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                WaitingForInput = false;
                InMenu = true;
                Titlecredits.SetActive(false);
                MenuScreen.SetActive(true);
            }
        }

        if (IntroEnded == false && InMenu == false)
        {
            FadeIn();
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                Fade = 0;
                Color ne = Fadescreen.color;
                ne.a = Fade;
                Fadescreen.color = ne;

                IntroEnded = true;
                AudioSource.volume = 0.5f;
                Titlecredits.SetActive(true);
                WaitingForInput = true;
            }
        }
        if (InMenu)
        {
            if (FadeOut)
            {
                ToLoadScreen();
            }
        }
        if (GetCamera && Cameras.Length > 0)
        {
            StartCoroutine(MoveCamera(CameraIndex));
        }
    }


    void FadeIn()
    {
        if (Fade > 0)
        {
            Fade -= 0.15f * Time.deltaTime;
            Color ne = Fadescreen.color;
            ne.a = Fade;
            Fadescreen.color = ne;
            MusicFade += 0.075f * Time.deltaTime;
            AudioSource.volume = MusicFade;
        }
        if (Fade <= 0)
        {
            FirstFade = false;
            IntroEnded = true;
            Titlecredits.SetActive(true);
            WaitingForInput = true;
        }
    }

    IEnumerator FadeOutCamera()
    {
        Fade = 0;
        for (int i = 0; i < 10; i++)
        {
            if (Fade < 1)
            {
                Fade += 0.1f;
                Color ne = Fadescreen.color;
                ne.a = Fade;
                Fadescreen.color = ne;
            }
            if (Fade > 1) { Fade = 1; }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator FadeInCamera()
    {
        Fade = 1;
        for (int i = 0; i < 10; i++)
        {
            if (Fade > 0)
            {
                Fade -= 0.1f;
                Color ne = Fadescreen.color;
                ne.a = Fade;
                Fadescreen.color = ne;
            }
            if (Fade < 0) { Fade = 0; }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ToLoadScreen()
    {
        if (Fade > 0)
        {
           
            Fade += 0.015f * Time.deltaTime;
            Color ne = Fadescreen.color;
            ne.a = Fade;
            Fadescreen.color = ne;
            MusicFade -= 0.05f * Time.deltaTime;
            AudioSource.volume = Fade;
        } else
        {
            SceneManager.LoadScene(GameSceneIndex);
        }
    }

    public void GOTOGAME()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    public void StartNewGame()
    {
        FadeOut = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator MoveCamera(int camera)
    {
        if (GetCamera)
        {
            GetCamera = false;
            OldCamPos = Cameras[CameraIndex].transform.position;
            CameraObject.transform.parent = Cameras[CameraIndex].transform;
            CameraObject.transform.localPosition = Vector3.zero;
        }
        //if (!FirstFade) { StartCoroutine(FadeInCamera()); }
        for(int i = 0; i < 10; i++)
        {
            for (int o = 0; o < 100; o++)
            {
                Cameras[CameraIndex].transform.position += (transform.forward * 0.25f) * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
            //if (i == 9) { StartCoroutine(FadeOutCamera()); }
        }

        CameraIndex++;
        if (CameraIndex == MaxCams) { CameraIndex = 0; }

        GetCamera = true;
        Debug.Log("Switching to new Camera");
        yield return new WaitForSeconds(2);
    }
}
