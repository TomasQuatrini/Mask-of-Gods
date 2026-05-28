using UnityEngine;



namespace System.Inventory
{
    [System.Serializable]
    public abstract class ItemData : ScriptableObject
    {
        public string ItemName;
        public string Id;
        public ItemType Type;
        public EffectType EffectType;
        public int EffectAmount;

        public bool stackable = true;
        public int maxStack = 99;

        public string IconPath;
        private Sprite _cachedIcon;

        public Sprite GetIcon()
        {
            if (_cachedIcon != null) return _cachedIcon;
            if (string.IsNullOrEmpty(IconPath))
            {
                return null;
            }
            _cachedIcon = UnityEngine.Resources.Load<Sprite>(IconPath);
            return _cachedIcon;
        }
        public abstract void Use(Inventory owner);
    }
    public class EquippableItemData : ItemData
    {
        bool stackable => false;

        public override void Use(Inventory owner)
        {

        }
    }
}
