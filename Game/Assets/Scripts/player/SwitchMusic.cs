using UnityEngine;
using System.Collections;

public class SwitchMusic : MonoBehaviour {
    public AudioClip Switchtothisclip;
    public float AudioVolume = 0.5f;
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
                        AudioSource.clip = Switchtothisclip;
                        AudioSource.Stop();
                        AudioSource.Play();
                    }
                } else
                {
                    AudioSource.clip = Switchtothisclip;
                    AudioSource.Play();
                }
                

            }
        }
    }
}
