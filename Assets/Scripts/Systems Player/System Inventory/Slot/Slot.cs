using UnityEngine;

namespace System.Inventory
{
    [System.Serializable]
    public class Slot
    {
        public ItemStack stack;
        public int limit;
        public bool IsEmpty => stack == null;

        public bool CanAccept(ItemData item)
        {
            if (IsEmpty) return true;
            return stack.item == item && item.stackable;
        }
       
        public int Add(ItemData item,
                       int amount)
        {
            if (IsEmpty)
            {
                int limit = item.stackable ? item.maxStack : 1;
                int toAdd = Mathf.Min(limit, amount);
                stack = new ItemStack(item, toAdd);
                return amount - toAdd;
            }
            if (stack.item != item)
            {
                return amount;
            }
            int limitCurrent = item.maxStack;
            int space = limitCurrent - stack.quantity;
            int added = Mathf.Min(space, amount);
            stack.quantity += added;
            return amount - added;
        }
    }
}