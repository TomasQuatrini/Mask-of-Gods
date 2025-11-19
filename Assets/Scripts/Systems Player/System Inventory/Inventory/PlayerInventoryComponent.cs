using UnityEngine;

namespace System.Inventory
{
    public class PlayerInventoryComponent : MonoBehaviour
    {
        [SerializeField] private int _consumableSlots = 4;
        [SerializeField] private int _equippableSlots = 2;
        [SerializeField] private int _maskSlots = 4;

        public Inventory Consumable { get; private set; }
        public Inventory Equippable { get; private set; }
        public Inventory Mask { get; private set; }

        public System.Action OnInventoryChanged;

        private void Awake()
        {
            Consumable = new Inventory(_consumableSlots);
            Equippable = new Inventory(_equippableSlots);
            Mask = new Inventory(_maskSlots);
        }

        public void AddItem(ItemData item, int quantity)
        {
            Inventory target = item.Type switch
            {
                ItemType.Consumable => Consumable,
                ItemType.Equippable => Equippable,
                ItemType.Mask => Mask,
            };
            bool ok = target.AddItem(item, quantity);
            if (!ok)
            {
                Debug.Log($"No existe espacio en el inventario de {item.Type}");
            }
            else
            {
                OnInventoryChanged?.Invoke();
                Debug.Log($"Se agrego {quantity} de {item.Name} en {item.Type}");
            }
        }
    }
}