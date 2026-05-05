using System.Collections.Generic;
namespace System.Inventory
{
    public class Inventory
    {
        public List<Slot> slots;
        public Inventory(int slotCount)
        {
            slots = new List<Slot>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                slots.Add(new Slot());
            }
        }
        public bool AddItem(ItemData item, int quantity)
        {
            int remaining = quantity;
            if (item.stackable)
            {
                foreach (var slot in slots)
                {
                    if (!slot.IsEmpty && slot.stack.item == item)
                    {
                        remaining = slot.Add(item, remaining);
                        if (remaining <= 0)
                            return true;
                    }
                }
            }
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    remaining = slot.Add(item, remaining);
                    if (remaining <= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveItem(ItemData item, int quantity)
        {
            int consume = quantity;
            if (item.stackable)
            {
                foreach (var slot in slots)
                {
                    if (slot.IsEmpty) continue;
                    if (slot.stack.item != item) continue;
                    slot.Remove(item, consume);
                    return true;
                }
            }
            return false;
        }

        public bool ConsumeItem(ItemData item, int count)
        {
            if (item == null) return false;
            if (item.Type != ItemType.Consumable) return false;
            RemoveItem(item, count);
            return true;
        }

        public int GetItemQuantity(ItemData item)
        {
            int quantity = 0;
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && slot.stack.item == item)
                {
                    quantity += slot.stack.quantity;
                }
            }
            return quantity;
        }

        public void CompactSlots()
        {
            List<ItemStack> stacks = new List<ItemStack>();
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty)
                {
                    stacks.Add(slot.stack);
                }
            }
            foreach (Slot slot in slots)
            {
                slot.Clear();
            }

            for (int i = 0; i < stacks.Count; i++)
            {
                slots[i].stack = stacks[i];
            }
        }
    }
    public enum InventoryType
    {
        Stackable,
        Equippable
    }
}