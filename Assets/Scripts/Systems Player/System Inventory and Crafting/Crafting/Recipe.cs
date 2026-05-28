using UnityEngine;
using System.Inventory;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]

[System.Serializable]
public class Recipe : ScriptableObject
{
    [SerializeField] private string _recipeName;
    [SerializeField] private List<ItemStack> ingredients = new List<ItemStack>();
    [SerializeField] private ItemStack result = new ItemStack();

    public string RecipeName => string.IsNullOrWhiteSpace(_recipeName) ? "Sin Nombre" : _recipeName;
    public List<ItemStack> Ingredients => ingredients;
    public ItemStack Result => result;

}