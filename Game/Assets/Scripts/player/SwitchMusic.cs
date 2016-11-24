using UnityEngine;
using System.Collections;

public class SwitchMusic : MonoBehaviour {
    public AudioClip Switchtothisclip;
    public float AudioVolume = 0.5f;
    private float currentvolume = 0;
    private AudioSource AudioSource;
    // Use this for initialization
    void Start()
    {
        AudioSource = GameObject.FindGameObjectWithTag("Player").transform.Find("MusicHandler").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (AudioSource.clip) {
                    if (AudioSource.clip.name != Switchtothisclip.name)
                    {
                        StartCoroutine(Switching());
                    }
                } else
                {
                    AudioSource.clip = Switchtothisclip;
                    AudioSource.Play();
                }
                

            }
        }
    }

    IEnumerator Switching()
    {
        currentvolume = 0.25f;
        for (int i = 0; i < 25;i++)
        {
            currentvolume -= 0.01f;
            AudioSource.volume = currentvolume;
            yield return new WaitForSeconds(0.05f);
        }
        AudioSource.Stop();
        AudioSource.clip = Switchtothisclip;
        AudioSource.Play();
        for (int i = 0; i < 25; i++)
        {
            currentvolume += 0.01f;
            AudioSource.volume = currentvolume;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
