namespace System.Inventory
{
    public interface IPickable
    {
        ItemData GetItemData();
        int GetQuantity();
        void OnPicked();
    }
}