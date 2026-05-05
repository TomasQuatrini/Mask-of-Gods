using UnityEngine;
using System.Inventory;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public List<ItemStack> ingredients;
    public ItemStack result;
}
