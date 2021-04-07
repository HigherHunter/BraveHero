using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//manages main menu buttons
public class MainMenu : MonoBehaviour
{

    public void Resume()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            if (File.Exists("equipmentItems.txt"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
            }
        }
    }

    public void NewGame()
    {
        DeleteFile("equipmentItems.txt");
        DeleteFile("inventoryItems.txt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
