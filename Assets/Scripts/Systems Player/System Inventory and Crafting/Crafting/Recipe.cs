using UnityEngine;
using System.Inventory;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<ItemStack> ingredients;
    [SerializeField] private ItemStack result;

    public IReadOnlyList<ItemStack> Ingredients => ingredients.AsReadOnly();
    public ItemStack Result => result;
}
