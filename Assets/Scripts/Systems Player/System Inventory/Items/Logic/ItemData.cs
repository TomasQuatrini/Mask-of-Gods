using UnityEngine;

namespace System.Inventory
{
    public abstract class ItemData : ScriptableObject
    {
        public string Name;
        public string Id;
        public ItemType Type;
        public EffectType EffectType;
        public int EffectAmount;

        public bool stackable = true;
        public int maxStack = 99;

        [Header("UI")]
        public Sprite Icon;

        public abstract void Use(Inventory owner); //inventory owner in parentesis
    }
}
