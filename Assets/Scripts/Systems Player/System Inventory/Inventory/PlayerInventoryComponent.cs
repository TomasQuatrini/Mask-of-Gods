using UnityEngine;
using System.Collections.Generic;

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
        public WeaponItemData EquippedWeapon { get; private set; }

        public System.Action OnInventoryChanged;
        public System.Action<WeaponItemData> OnWeaponEquipped;


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
            if (slotIndex < 0 || slotIndex >= Equippable.slots.Count)
            {
                return false;
            }
            var slot = Equippable.slots[slotIndex];
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
            foreach (var slot in Equippable.slots)
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
    }
}