using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
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

        public bool RemoveItem(ItemData item)
        {
            int consume = 1;
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
    }
}