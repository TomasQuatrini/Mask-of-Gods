using UnityEngine;
using System.Inventory;


public class CraftingSystem : MonoBehaviour, ICraftingSystem
{
    [SerializeField] private PlayerInventoryComponent _inventoryComponent;

    public bool CanCraft(Recipe recipe)
    {
        if (recipe == null)
        {
            Debug.LogError("CraftingSystem: Recipe is null.");
            return false;
        }

        if (_inventoryComponent == null)
        {
            Debug.LogError("CraftingSystem: InventoryComponent no asignado.", this);
            return false;
        }

        foreach (var ingredient in recipe.Ingredients)
        {
            if (ingredient == null)
            {
                Debug.LogError($"CraftingSystem: Recipe {recipe.RecipeName} tiene un ingrediente null.", recipe);
                return false;
            }

            if (ingredient.item == null)
            {
                Debug.LogError($"CraftingSystem: Recipe {recipe.RecipeName} tiene un ingredient.item null.", recipe);
                return false;
            }

            if (ingredient.quantity <= 0)
            {
                Debug.LogError($"CraftingSystem: Recipe {recipe.RecipeName} tiene cantidad inválida.", recipe);
                return false;
            }

            if (!_inventoryComponent.HasItem(ingredient.item, ingredient.quantity))
            {
                Debug.Log($"CraftingSystem: Missing ingredient {ingredient.item.ItemName} x{ingredient.quantity}.");
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

        if (recipe.Result == null || recipe.Result.item == null || recipe.Result.quantity <= 0)
        {
            Debug.LogError($"CraftingSystem: Recipe {recipe.RecipeName} tiene result inválido.", recipe);
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

