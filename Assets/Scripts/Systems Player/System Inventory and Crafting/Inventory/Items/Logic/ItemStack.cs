namespace System.Inventory
{
    [System.Serializable]
    public class ItemStack
    {
        public ItemData item;
        public int quantity;
        public bool isEmpty => item == null || quantity <= 0;


        public ItemStack(ItemData item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}