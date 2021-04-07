using UnityEngine;

//manages music and voices
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioClip voice, music;
    public AudioSource voiceSource, musicSource;

    // Use this for initialization
    void Start()
    {
        PlayVoice();
        PlayMusic();
    }

    //plays given voice mp3 
    public void PlayVoice()
    {
        if (voice != null)
        {
            voiceSource.clip = voice;
            voiceSource.Play();
        }
    }

    //plays given music mp3
    void PlayMusic()
    {
        if (music != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }
}
