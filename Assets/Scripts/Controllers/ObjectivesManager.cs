using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//manages objective info
public class ObjectivesManager : MonoBehaviour
{

    public static ObjectivesManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject objectivesPanel;

    Text text;
    int index;

    // Use this for initialization
    void Start()
    {
        text = objectivesPanel.transform.GetChild(0).GetComponent<Text>();
        index = SceneManager.GetActiveScene().buildIndex;
        DrawDefaultText(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Objective"))
        {
            objectivesPanel.SetActive(!objectivesPanel.activeSelf);
        }
    }
    //text to draw on start of the level
    public void DrawDefaultText(int index)
    {
        switch (index)
        {
            case 1:
                text.text = "Find the portal.";
                break;
            case 2:
                text.text = "Find the portal.";
                break;
            case 3:
                text.text = "Defeat the orc warlord!";
                break;
            case 4:
                text.text = "Reach the light.";
                break;
        }
    }
    //draw speficis text
    public void DrawText(string info)
    {
        text.text = info;
    }
}
