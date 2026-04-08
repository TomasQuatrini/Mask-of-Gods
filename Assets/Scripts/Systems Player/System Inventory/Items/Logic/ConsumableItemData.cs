using UnityEngine;

namespace System.Inventory
{ 
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Items/Consumable Item")]
    [System.Serializable]
    public class ConsumableItemData : ItemData
    {
        [Header("Consumable Item Data")]
        public int Amount;
        public ConsumableType Type_Consumable;
        new bool stackable = true;

        public bool Stackable { get => stackable; set => stackable = value; }        

        public override void Use(Inventory owner) //inventory owner in parentesis
        {
            owner.ConsumeItem(this);
        }
    }
}