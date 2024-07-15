using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource gameAudio;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void changeSound(AudioClip clip)
    {
        gameAudio.clip = clip;
    }

    public void playSound()
    {
        gameAudio.Play();
    }

    public void stopSound()
    {
        gameAudio.Stop();
    }
}
