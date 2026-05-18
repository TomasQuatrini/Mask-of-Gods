using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class RecipeButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _recipeNameText;
    private Recipe _recipe;
    private Action<Recipe> _onClick;

    public void Setup(Recipe recipe, Action<Recipe> onClick)
    {
        _recipe = recipe;
        _onClick = onClick;
        _recipeNameText.text = recipe.recipeName;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(CLick);
    }

    public void CLick()
    {
        _onClick?.Invoke(_recipe);
    }
}
