using UnityEngine;

namespace System.Inventory
{
    public class PlayerItemCollector : MonoBehaviour
    {
        private PlayerInventoryComponent _inventoryComponent;
        private PlayerContext _ctx;
        private void Awake()
        {
            _ctx = GetComponentInParent<PlayerContext>();
            _inventoryComponent = _ctx.Inventory;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPickable>(out var pickable))
            {
                ItemData itemData = pickable.GetItemData();
                int quantity = pickable.GetQuantity();
                bool added = _inventoryComponent.AddItem(itemData, quantity);
                if (added)
                {
                    pickable.OnPicked();
                }
            }
        }
    }
}