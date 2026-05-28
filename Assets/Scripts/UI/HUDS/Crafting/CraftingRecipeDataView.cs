using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Inventory;
public class CraftingRecipeDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultNameText;
    [SerializeField] private Transform _ingredientsParent;
    [SerializeField] private IngredientView _ingredientPrefab;
    [SerializeField] private Button _craftButton;

    public void Show(Recipe recipe, PlayerInventoryComponent inventory, bool canCraft, Action onCraft)
    {
        if (recipe == null)
        {
            Clear();
            return;
        }

        _resultNameText.text = recipe.RecipeName;

        foreach (Transform child in _ingredientsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var ingredient in recipe.Ingredients)
        {
            if (ingredient == null || ingredient.item == null)
            {
                Debug.LogError($"CraftingRecipeDataView: Recipe {recipe.RecipeName} tiene ingrediente vacío.", recipe);
                continue;
            }

            IngredientView view = Instantiate(_ingredientPrefab, _ingredientsParent);
            int currentAmount = inventory != null ? inventory.GetItemQuantity(ingredient.item) : 0;
            int requiredAmount = ingredient.quantity;

            view.Setup(ingredient.item.ItemName, currentAmount, requiredAmount);
        }

        _craftButton.interactable = canCraft;
        _craftButton.onClick.RemoveAllListeners();
        _craftButton.onClick.AddListener(() => onCraft?.Invoke());
    }

    public void Clear()
    {
        _resultNameText.text = "";

        foreach (Transform child in _ingredientsParent)
        {
            Destroy(child.gameObject);
        }

        _craftButton.interactable = false;
        _craftButton.onClick.RemoveAllListeners();
    }
}
