using UnityEngine;
using System.Collections.Generic;

namespace System.Inventory
{
    public class PlayerInventoryComponent : MonoBehaviour
    {
        [SerializeField] private int _consumableSlots = 4;
        [SerializeField] private int _equippableSlots = 2;

        private Inventory _consumable;
        private Inventory _equippable;
        public WeaponItemData EquippedWeapon { get; private set; }

        public IReadOnlyCollection<Slot> ConsumableSlots => _consumable.slots.AsReadOnly();
        public IReadOnlyCollection<Slot> EquippableSlots => _equippable.slots.AsReadOnly();

        public System.Action OnInventoryChanged;
        public System.Action<WeaponItemData> OnWeaponEquipped;


        private void Awake()
        {
            _consumable = new Inventory(_consumableSlots);
            _equippable = new Inventory(_equippableSlots);
        }

        public bool AddItem(ItemData item, int quantity)
        {
            if (item == null)
            {
                return false;
            }
            Inventory target;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = _consumable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
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
                return false;
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
            Inventory target;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = _consumable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
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

        public bool HasItem(ItemData item)
        {
            if (item == null)
            {
                return false;
            }
            Inventory target;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = _consumable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
                    break;
                default:
                    return false;
            }
            if (target == null)
            {
                return false;
            }
            foreach (var slot in target.slots)
            {
                if (!slot.IsEmpty && slot.stack.item == item)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetItemQuantity(ItemData item)
        {
            if (item == null)
            {
                return 0;
            }
            Inventory target;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = _consumable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
                    break;
                default:
                    return 0;
            }
            if (target == null)
            {
                return 0;
            }
            foreach (var slot in target.slots)
            {
                if (!slot.IsEmpty && slot.stack.item == item)
                {
                    return slot.stack.quantity;
                }
            }
            return 0;
        }

        public Inventory GetInventory(ItemType type)
        {
            switch (type)
            {
                case ItemType.Consumable:
                    return _consumable;
                case ItemType.Equippable:
                    return _equippable;
                default:
                    return null;
            }
        }

        #region Weapon Management

        public bool EquipWeapon(WeaponItemData weapon)
        {
            if (weapon == null)
            {
                return false;
            }
            EquippedWeapon = weapon;
            OnWeaponEquipped?.Invoke(weapon);
            OnInventoryChanged?.Invoke();
            return true;
        }

        public void UnequipWeapon()
        {
            EquippedWeapon = null;
            OnWeaponEquipped?.Invoke(null);
            OnInventoryChanged?.Invoke();
        }

        public bool EquipWeaponFromEquippableSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _equippable.slots.Count)
            {
                return false;
            }
            var slot = _equippable.slots[slotIndex];
            if (slot.IsEmpty || !(slot.stack.item is WeaponItemData weapon))
            {
                return false;
            }
            Debug.Log($"[PlayerInventoryComponent] Equipped weapon {weapon.name} from slot {slotIndex}");
            return EquipWeapon(weapon);           
        }

        public List<WeaponItemData> GetWeaponsInInventory()
        {
            List<WeaponItemData> weapons = new List<WeaponItemData>();
            foreach (var slot in _equippable.slots)
            {
                if (!slot.IsEmpty && slot.stack.item is WeaponItemData weapon)
                {
                    weapons.Add(weapon);
                }
            }
            return weapons;
        }

        public bool EquipNextWeapon()
        {
            List<WeaponItemData> weapons = GetWeaponsInInventory();
            if (weapons == null || weapons.Count == 0)
            {
                return false;
            }
            if (EquippedWeapon == null)
            {
                return EquipWeapon(weapons[0]);
            }
            int currentIndex = weapons.IndexOf(EquippedWeapon);
            if (currentIndex == -1)
            {
                return EquipWeapon(weapons[0]);
            }
            int nextIndex = (currentIndex + 1) % weapons.Count;
            return EquipWeapon(weapons[nextIndex]);
        }

        public bool EquipPreviousWeapon()
        {
            List<WeaponItemData> weapons = GetWeaponsInInventory();
            if (weapons == null || weapons.Count == 0)
            {
                return false;
            }
            if (EquippedWeapon == null)
            {
                return EquipWeapon(weapons[0]);
            }
            int currentIndex = weapons.IndexOf(EquippedWeapon);
            if (currentIndex == -1)
            {
                return EquipWeapon(weapons[0]);
            }
            int previousIndex = (currentIndex - 1 + weapons.Count) % weapons.Count;
            return EquipWeapon(weapons[previousIndex]);
        }

        #endregion
    }
}