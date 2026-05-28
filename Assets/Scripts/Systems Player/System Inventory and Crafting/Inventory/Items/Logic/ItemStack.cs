namespace System.Inventory
{
    [System.Serializable]
    public class ItemStack
    {
        public ItemData item;
        public int quantity = 1;

        public bool IsEmpty => item == null || quantity <= 0;

        public ItemStack() { }

        public ItemStack(ItemData item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}