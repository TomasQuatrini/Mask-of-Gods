using UnityEngine;

namespace System.Inventory
{
    [CreateAssetMenu(fileName = "New Mask Item", menuName = "Inventory/Items/Mask Item")]
    [System.Serializable]
    public class MaskItemData : ItemData
    {
        // Add properties specific to Mask items here
        //enum ConsumableType { get; set; } // e.g., Health, Mana, Stamina, etc.

        public override void Use() //inventory owner in parentesis
        {
            // owner.EquipMask(this);
        }
    }
}