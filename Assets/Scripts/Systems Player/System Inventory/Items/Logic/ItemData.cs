using UnityEngine;

namespace System.Inventory
{
    public abstract class ItemData : ScriptableObject
    {
        public string Name;
        public string Id;
        public ItemType Type;      

        public bool stackable = true;
        public int maxStack = 99;

        public abstract void Use(); //inventory owner in parentesis
    }
}
