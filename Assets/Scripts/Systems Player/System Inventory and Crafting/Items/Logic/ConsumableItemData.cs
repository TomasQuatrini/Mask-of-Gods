using UnityEngine;

namespace System.Inventory
{ 
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Items/Consumable Item")]
    [System.Serializable]
    public class ConsumableItemData : ItemData
    {       
        public override void Use(Inventory owner) //inventory owner in parentesis
        {
            owner.ConsumeItem(this);
        }
    }
}