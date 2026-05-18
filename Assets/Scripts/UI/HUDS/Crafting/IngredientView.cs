using UnityEngine;
using TMPro;

public class IngredientView : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;
    public void Setup(string itemName, int currentAmount, int requiredAmount)
    {
        _itemNameText.text = $"{itemName}: {currentAmount}/{requiredAmount}";
    }
}
