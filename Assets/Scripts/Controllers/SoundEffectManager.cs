using UnityEngine;

//manages sound effects (hit and death sound)
public class SoundEffectManager : MonoBehaviour
{

    public AudioClip[] hitSound;
    public AudioClip deathSound;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<CharacterCombat>().OnHitSoundTrigger += OnHitSoundTrigger;
        GetComponent<CharacterStats>().OnDeathSoundTrigger += OnDeathSoundTrigger;
    }

    //play death sound if triggered
    public void OnDeathSoundTrigger()
    {
        audioSource.Stop();
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    //play hit sound if triggered
    public void OnHitSoundTrigger(string tag)
    {
        switch (tag)
        {
            case "Skeleton":
                audioSource.clip = hitSound[0];
                audioSource.Play();
                break;
            case "Spider":
                audioSource.clip = hitSound[1];
                audioSource.Play();
                break;
            case "SpiderQueen":
                audioSource.clip = hitSound[1];
                audioSource.Play();
                break;
            case "Boss":
                audioSource.clip = hitSound[0];
                audioSource.Play();
                break;
            case "Player":
                audioSource.clip = hitSound[0];
                audioSource.Play();
                break;
        }
    }
}
