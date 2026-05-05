using UnityEngine;

namespace System.Inventory
{
    [CreateAssetMenu(fileName = "New Equippable Item", menuName = "Inventory/Items/Equippable Item")]
    [System.Serializable]
    public class EquippableItemData : ItemData
    {
        public override void Use(Inventory owner) //inventory owner in parentesis
        {
            // owner.EquipItem(this);
        }
    }
}