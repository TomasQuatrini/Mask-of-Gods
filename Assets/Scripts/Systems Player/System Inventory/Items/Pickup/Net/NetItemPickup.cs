using Fusion;
using UnityEngine;

namespace System.Inventory
{
    public class NetItemPickup : NetworkBehaviour, IPickable
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private int _quantity = 1;

        public ItemData GetItemData() => _itemData;
        public int GetQuantity() => _quantity;
        public void OnPicked()
        {
            if (Object != null && Object.Runner != null)
            {
                Object.Runner.Despawn(Object);
            }
            else
            {
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_itemData != null && _quantity < 1)
                _quantity = 1;
        }
#endif
    }
}