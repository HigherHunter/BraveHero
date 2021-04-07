using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//every slot in inventory, with mouse pointer action
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image icon;
    public Button removeButton;
    public GameObject itemInfo;

    Item item;
    Text itemName, armor, damage;

    //add new item
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    //clears slot for ui
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    //remove item from inventory
    public void OnRemoveButtonPressed()
    {
        Inventory.instance.Remove(item);
        itemInfo.SetActive(false);
    }

    //use item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            itemInfo.SetActive(false);
        }
    }

    //get item info when mouse pointer is on top
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item is Equipment)
        {
            Equipment equipment = (Equipment)item;
            itemInfo.SetActive(!itemInfo.activeSelf);
            itemName = itemInfo.transform.GetChild(0).GetComponent<Text>();
            armor = itemInfo.transform.GetChild(1).GetComponent<Text>();
            damage = itemInfo.transform.GetChild(2).GetComponent<Text>();
            itemName.text = "Name: " + equipment.name;
            armor.text = "Armor: " + equipment.armorModifier;
            damage.text = "Damage: " + equipment.damageModifier;
        }
    }

    //mouse pointer not on top
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfo.SetActive(false);
    }
}
