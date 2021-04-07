using UnityEngine;

//base class for every item
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        // use it 
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

