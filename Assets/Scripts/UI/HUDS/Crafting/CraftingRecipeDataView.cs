using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Inventory;

namespace UI.Crafting
{
    public class CraftingRecipeDataView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultNameText;
        [SerializeField] private Transform _ingredientsParent;
        [SerializeField] private IngredientView _ingredientPrefab;
        [SerializeField] private Button _craftButton;

        public void Show(Recipe recipe, PlayerInventoryComponent inventory, bool canCraft, Action onCraft)
        {
            Debug.Log($"Showing recipe: {recipe.name}, Can Craft: {canCraft}");
            _resultNameText.text = recipe.name;
            foreach (Transform child in _ingredientsParent)
            {
                Destroy(child.gameObject);
            }
            foreach (var ingredient in recipe.Ingredients)
            {
                IngredientView view = Instantiate(_ingredientPrefab, _ingredientsParent);
                int currentAmount = inventory.GetItemQuantity(ingredient.item);
                int requiredAmount = ingredient.quantity;
                view.Setup(ingredient.item.name, currentAmount, requiredAmount);
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
}
