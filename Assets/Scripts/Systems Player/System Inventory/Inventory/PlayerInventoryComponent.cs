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
            if (item == null)
            { 
                return false; 
            }
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
                    return false;
            }
            if (target == null)
            {
                return false;
            }
            bool added = target.AddItem(item, quantity);            
            if (!added)
            {
                return false ;
            }
            OnInventoryChanged?.Invoke();
            return true;
        }

        public bool ConsumeItem(ItemData item)
        {
            if (item == null) 
            { 
                return false;
            }
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
                    return false;
            }
            if (target == null) 
            { 
                return false;
            }
            bool removed = target.ConsumeItem(item);
            OnInventoryChanged?.Invoke();
            return removed;
        }
    }
}