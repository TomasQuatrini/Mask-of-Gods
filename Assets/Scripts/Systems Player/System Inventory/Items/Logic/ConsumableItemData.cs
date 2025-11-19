using UnityEngine;

namespace System.Inventory
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Items/Consumable Item")]
    [System.Serializable]
    public class ConsumableItemData : ItemData
    {
        public int Amount;
        new bool stackable = true;

        public bool Stackable { get => stackable; set => stackable = value; }

        //enum ConsumableType { get; set; } // e.g., Health, Mana, Stamina, etc.

        public override void Use() //inventory owner in parentesis
        {
            // owner.ConsumeItem(this);
        }
    }
}