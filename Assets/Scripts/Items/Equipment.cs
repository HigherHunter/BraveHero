using UnityEngine;

//all equipments
[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
public class Equipment : Item
{

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    //bouns armor from equipment
    public int armorModifier;
    //bonus damage from equipment
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        // equip and remove from the inventory
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

}

//all possible equipment types
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
//regions that equipment covers
public enum EquipmentMeshRegion { Legs, Arms, Torso };