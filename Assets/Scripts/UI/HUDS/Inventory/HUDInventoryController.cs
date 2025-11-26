using UnityEngine;

namespace System.Inventory
{

    public class HUDInventoryController : MonoBehaviour
    {
        public static HUDInventoryController Instance { get; private set; }

        private UIInventorySlotView[] _slotViews;
        private PlayerInventoryComponent _inventory;

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            _slotViews = GetComponentsInChildren<UIInventorySlotView>();
            if (_slotViews == null || _slotViews.Length == 0)
            {
                Debug.LogError("UIInventoryHudController: No slot views found in children.");
            }
        }
        public void Bind(PlayerInventoryComponent inventory)
        {
            _inventory = inventory;
            if (_inventory == null)
            {
                Debug.LogError("UIInventoryHudController: Slot views not assigned.");
                return;
            }
            _inventory.OnInventoryChanged += Refresh;
            Refresh();
        }
        private void OnDestroy()
        {
            if (_inventory != null)
            {
                _inventory.OnInventoryChanged -= Refresh;
            }
        }
        private void Refresh()
        {
            if (_inventory == null)
            {
                Debug.LogError("UIInventoryHudController: Inventory is null on Refresh.");
                return;
            }
            var source = _inventory.Consumable; //fijarse como variar entre diferentes inventarios

            for (int i = 0; i < _slotViews.Length; i++)
            {
                var view = _slotViews[i];
                if (source != null && i < source.slots.Count && !source.slots[i].IsEmpty)
                {
                    //Debug.Log($"[HUDInventoryController] Showing slot {i} with stack {source.slots[i].stack}");
                    var slot = source.slots[i];
                    view.Show(slot.stack);
                }
                else
                {
                    //Debug.Log($"[HUDInventoryController] Showing empty slot {i}");
                    view.ShowEmpty();
                }
            }
        }
    }
}