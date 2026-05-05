using UnityEngine;
using System.Inventory;


public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private PlayerInventoryComponent _inventoryComponent;

    public bool CanCraft(RecipeData recipe)
    {
        if (recipe == null)
        {
            Debug.LogError("CraftingSystem: Recipe is null in CanCraft.");
            return false;
        }

        // validar ingredientes
        return true;
    }

    public bool TryCraft(RecipeData recipe)
    {
        if (!CanCraft(recipe))
        {
            return false;
        }
        // consumir ingredientes
        // agregar resultado al inventario
        return true;
    }
}
