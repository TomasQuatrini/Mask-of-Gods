using System.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Materials/Material Item")]
public class MaterialItemData : ItemData
{
    public override void Use(Inventory owner)
    {
        // no contain
    }
}
