using UnityEngine;
using System.Collections.Generic;

namespace System.Inventory
{
    public class PlayerInventoryComponent : MonoBehaviour
    {
        [SerializeField] private int _stackableSlots = 6;
        [SerializeField] private int _equippableSlots = 3;

        private Inventory _stackable;
        private Inventory _equippable;
        
        public WeaponItemData EquippedWeapon { get; private set; }

        public IReadOnlyCollection<Slot> ConsumableSlots => _stackable.slots.AsReadOnly();
        public IReadOnlyCollection<Slot> EquippableSlots => _equippable.slots.AsReadOnly();

        public System.Action OnInventoryChanged;
        public System.Action<WeaponItemData> OnWeaponEquipped;


        private void Awake()
        {
            _stackable = new Inventory(_stackableSlots);
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
                    target = _stackable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
                    break;
                case ItemType.Material:
                    target = _stackable;
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

        public bool ConsumeItem(ItemData item, int quantity)
        {
            if (item == null)
            {
                return false;
            }
            Inventory target;
            switch (item.Type)
            {
                case ItemType.Consumable:
                    target = _stackable;
                    break;
                case ItemType.Equippable:
                    target = _equippable;
                    break;
                case ItemType.Material:
                    target = _stackable;
                    break;
                default:
                    return false;
            }
            if (target.GetItemQuantity(item) < quantity)
            {
                return false;
            }
            if (target == null)
            {
                return false;
            }
            bool removed = target.ConsumeItem(item, quantity);
            OnInventoryChanged?.Invoke();
            return removed;
        }

        public bool HasItem(ItemData item, int quantity)
        {
            if (item == null || quantity <= 0)
            {
                return false;
            }
            Inventory target = GetInventory(item.Type);
            if (target == null)
            {
                return false;
            }
            return GetItemQuantity(item) >= quantity;
        }

        public int GetItemQuantity(ItemData item)
        {
            if (item == null)
            {
                return 0;
            }
            Inventory target = GetInventory(item.Type);
            if (target == null)
            {
                return 0;
            }
            return target.GetItemQuantity(item);
        }

        public bool RemoveItem(ItemData item, int quantity)
        {
            if (!HasItem(item, quantity))
            {
                return false;
            }
            Inventory target = GetInventory(item.Type);
            if (target == null)
            {
                return false;
            }
            bool removed = target.RemoveItem(item, quantity);
            if (removed)
            {
                target.CompactSlots();
                OnInventoryChanged?.Invoke();
            }
            return removed;
        }

        public Inventory GetInventory(ItemType type)
        {
            switch (type)
            {
                case ItemType.Material:
                    return _stackable;
                case ItemType.Equippable:
                    return _equippable;
                case ItemType.Consumable:
                    return _stackable;
                default:
                    return null;
            }
        }

        public Inventory GetInventoryByType(InventoryType type)
        {
            if (type == InventoryType.Stackable)
            {
                return _stackable;
            }
            if (type == InventoryType.Equippable)
            {
                return _equippable;
            }
            return null;
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