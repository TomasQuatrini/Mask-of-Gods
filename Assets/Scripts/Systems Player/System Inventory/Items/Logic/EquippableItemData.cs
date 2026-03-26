using UnityEngine;

namespace System.Inventory
{
    [CreateAssetMenu(fileName = "New Equippable Item", menuName = "Inventory/Items/Equippable Item")]
    [System.Serializable]
    public class EquippabbleItemData : ItemData
    {
        // Add properties specific to equippable items here
        new bool stackable = false;
        public bool Stackable { get => stackable; set => stackable = value; }

        public override void Use(Inventory owner) //inventory owner in parentesis
        {
            // owner.EquipItem(this);
        }
    }
}