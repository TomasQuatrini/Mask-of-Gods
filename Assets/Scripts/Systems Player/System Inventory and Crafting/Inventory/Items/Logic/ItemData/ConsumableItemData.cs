using System.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Items/Consumable/Consumable Item")]
[System.Serializable]
public class ConsumableItemData : ItemData
{
    public override void Use(Inventory owner) //inventory owner in parentesis
    {
        
    }
}