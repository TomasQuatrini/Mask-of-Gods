using UnityEngine;
using System.Inventory;


public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private PlayerInventoryComponent _inventoryComponent;

    public bool CanCraft(Recipe recipe)
    {
        if (recipe == null)
        {
            Debug.Log("CraftingSystem: Recipe is null.");

            return false;
        }
        foreach (var ingredient in recipe.Ingredients)
        {
            if (!_inventoryComponent.HasItem(ingredient.item, ingredient.quantity))
            {
                Debug.Log($"CraftingSystem: Missing ingredient {ingredient.item.name} x{ingredient.quantity}.");
                return false;
            }
        }
        return true;
    }

    public bool TryCraft(Recipe recipe)
    { 
        if (!CanCraft(recipe))
        {
            Debug.Log("CraftingSystem: Cannot craft, missing ingredients.");
            return false;
        }
        foreach (var ingredient in recipe.Ingredients)
        {
            _inventoryComponent.RemoveItem(ingredient.item, ingredient.quantity);
        }        
        _inventoryComponent.AddItem(recipe.Result.item, recipe.Result.quantity);
        return true;
    }
}
