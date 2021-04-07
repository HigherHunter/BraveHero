using UnityEngine;
using UnityEngine.SceneManagement;

//controls world triggers
public class TriggerCheck : MonoBehaviour
{
    public string text;

    private void OnTriggerEnter(Collider other)
    {
        string tag = gameObject.tag;
        string otherTag = other.gameObject.tag;
        switch (tag)
        {
            case "Pick":
                if (otherTag == "Player")
                {
                    Destroy(gameObject);
                }
                break;
            case "LevelEnd":
                if (otherTag == "Player")
                {
                    SaveLoadManager.instance.Save();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case "Pass":
                if (otherTag == "Player")
                {
                    ObjectivesManager.instance.DrawText(text);
                    SoundManager.instance.voice = Resources.Load("Sounds/Voices/Part2.1") as AudioClip;
                    SoundManager.instance.PlayVoice();
                    Destroy(gameObject);
                }
                break;
            case "End":
                SceneManager.LoadScene(0);
                break;
        }
    }
}
