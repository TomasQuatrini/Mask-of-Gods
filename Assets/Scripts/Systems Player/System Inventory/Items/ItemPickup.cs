using UnityEngine;

namespace System.Inventory
{
    public class ItemPickup : MonoBehaviour, IPickable
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private int _quantity = 1;

        public ItemData GetItemData() => _itemData;
        public int GetQuantity() => _quantity;
        public void OnPicked()
        {
            Destroy(gameObject);
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