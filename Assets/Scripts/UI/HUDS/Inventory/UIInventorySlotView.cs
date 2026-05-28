using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace System.Inventory
{
    public class UIInventorySlotView : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _quantityText;

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            if ( _icon == null )
                {
                    Debug.LogError("UIInventorySlotView: No Image component found in children.", this);
                }
            _quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowEmpty()
        {
            _icon.enabled = false;
            _quantityText.text = string.Empty;
        }

        public void Show(ItemStack stack)
        {
            if (stack == null || stack.item == null)
            {
                ShowEmpty();
                return;
            }
            _icon.enabled = true;
            _icon.sprite = stack.item.GetIcon();
            if (stack.item.stackable && stack.quantity > 0)
            {
                _quantityText.text = stack.quantity.ToString();
            }
            else
            {
                _quantityText.text = string.Empty;
            }
        }
    }
}