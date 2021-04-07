using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//players inventory
public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    // subscribe events to delegate and call on trigger
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 20;

    public List<Item> items = new List<Item>();

    //add item to inventory
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                // no more room
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
        return true;
    }

    //remove item from inventory
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
