using System.Inventory;
using UnityEngine;
using System.Collections.Generic;

public class HUD_CraftingUI : MonoBehaviour
{
    [Header("Root")]
    [SerializeField] private GameObject _panel;

    [Header("Recipe List")]
    [SerializeField] private Transform _recipeButtonParent;
    [SerializeField] private RecipeButtonView _recipeButtonPrefab;

    [Header("Recipe Details")]
    [SerializeField] private CraftingRecipeDataView _craftingRecipeDataView;

    private PlayerInventoryComponent _inventory;
    private ICraftingSystem _craftingSystem;
    private Recipe[] _availableRecipes;
    private Recipe _selectedRecipe;

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void Open(PlayerContext playerContext, Recipe[] availableRecipes)
    {
        _inventory = playerContext.Inventory;
        _craftingSystem = playerContext.CraftingSystem;
        _availableRecipes = availableRecipes;
        _panel.SetActive(true);
        BuildRecipeButtons();
        if (_availableRecipes.Length > 0)
        {
            SelectRecipe(_availableRecipes[0]);
        }
    }

    public void Close()
    {
        _panel.SetActive(false);
    }

    private void SelectRecipe(Recipe recipe)
    {
        _selectedRecipe = recipe;
        Debug.Log($"Selected recipe: {recipe.RecipeName}");
        RefreshDetail();
    }

    private void BuildRecipeButtons()
    {
        foreach (Transform child in _recipeButtonParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var recipe in _availableRecipes)
        {
            var button = Instantiate(_recipeButtonPrefab, _recipeButtonParent);
            button.Setup(recipe, SelectRecipe);
        }
    }

    private void RefreshDetail()
    {
        if (_selectedRecipe == null)
        {
            Debug.LogWarning("No recipe selected to refresh details.");
            _craftingRecipeDataView.Clear();
            return;
        }
        Debug.Log($"Refreshing details for recipe: {_selectedRecipe.RecipeName}");
        bool canCraft = _craftingSystem.CanCraft(_selectedRecipe);
        _craftingRecipeDataView.Show(_selectedRecipe, _inventory, canCraft, CraftSelected);
    }

    private void CraftSelected()
    {
        if (_selectedRecipe == null) return;
        bool crafted = _craftingSystem.TryCraft(_selectedRecipe);

        if (crafted)
        {
            RefreshDetail();
        }
        else
        {
            UI_Message.Instance.ShowErrorMessage("No tienes los materiales necesarios para crear este objeto.");
        }
    }
}
