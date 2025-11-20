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

        public bool AddItem(ItemData item, int quantity)
        {
            Debug.Log($"Intentando agregar {quantity} de {item.Name} en {item.Type}");
            if (item == null)
            { 
                Debug.LogError("Item es null al intentar agregar al inventario"); 
                return false; 
            }
            Debug.Log($"Item {item.Name} de tipo {item.Type}");
            Inventory target = null;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = Consumable;
                    break;
                case ItemType.Equippable:
                    target = Equippable;
                    break;
                case ItemType.Mask:
                    target = Mask;
                    break;
                    default:
                    Debug.LogError($"Tipo de item {item.Type} no soportado en el inventario del jugador");
                    return false;
            }
            
            if (target == null)
            {
                Debug.LogError("Target inventory es null al intentar agregar item");
                return false;
            }
            Debug.Log($"Agregando {quantity} de {item.Name} en inventario de {item.Type}");
            bool added = target.AddItem(item, quantity);
            Debug.Log(added
                ? $"Se agregaron {quantity} de {item.Name} al inventario de {item.Type}"
                : $"No se pudo agregar {item.Name} al inventario de {item.Type}");
            if (!added)
            {
                Debug.LogWarning($"No se pudo agregar {item.Name} al inventario de {item.Type}");
                return false ;
            }
            OnInventoryChanged?.Invoke();
            return true;
        }
    }
}