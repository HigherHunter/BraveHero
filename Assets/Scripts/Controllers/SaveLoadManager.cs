using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//manages saving and loading
public class SaveLoadManager : MonoBehaviour
{

    public static SaveLoadManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(LoadEverything(0.5f));
        StartCoroutine(SaveEverything(1));
    }

    //writes equiped items and inventory items to file
    public void Save()
    {
        StreamWriter file;
        List<Item> items = Inventory.instance.items;
        if (items != null)
        {
            file = new StreamWriter("inventoryItems.txt");
            foreach (Item item in items)
            {
                file.WriteLine(item.name);
            }
            file.Close();
        }

        Equipment[] equipment = EquipmentManager.instance.currentEquipment;
        if (equipment.Length != 0)
        {
            file = new StreamWriter("equipmentItems.txt");
            foreach (Equipment item in equipment)
            {
                if (item != null)
                {
                    file.WriteLine(item.name);
                }
                else
                {
                    file.WriteLine("null");
                }
            }
            file.Close();
        }
        //writes current level
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex);
    }

    //loads equiped items and iventory items from file
    public void Load()
    {
        StreamReader file;
        string line;
        if (File.Exists("inventoryItems.txt"))
        {
            file = new StreamReader("inventoryItems.txt");
            while ((line = file.ReadLine()) != null)
            {
                Inventory.instance.Add(Resources.Load("Items/" + line) as Item);
            }
            file.Close();
        }

        if (File.Exists("equipmentItems.txt"))
        {
            file = new StreamReader("equipmentItems.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (!line.Equals("null"))
                {
                    EquipmentManager.instance.Equip(Resources.Load("Items/" + line) as Equipment);
                }
            }
            file.Close();
        }
    }

    IEnumerator LoadEverything(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Load();
    }

    IEnumerator SaveEverything(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Save();
    }
}
