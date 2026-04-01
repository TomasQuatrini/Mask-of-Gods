using UnityEngine;
using System.Inventory;

public class ItemConsume : MonoBehaviour
{
    private PlayerInventoryComponent _inventory;
    private InputPlayer _input;
    private PlayerContext _ctx;
    private PlayerHealth _health;
    private PlayerStaminaSM _stamina;

    private void Awake()
    {
        _input = InputPlayer.Instance;
        if (_input == null)
        {
            Debug.LogError("InputPlayer instance not found in the scene.");
        }
        _ctx = GetComponentInParent<PlayerContext>();
        if (_ctx == null) { Debug.LogError("PlayerContext don't exist"); return; }
        _inventory = _ctx.Inventory;
        _health = _ctx.Health;
        _stamina = _ctx.Stamina;
    }

    private void Start()
    {
        _input.ConsumeHealthPotion += ConsumeHealthPotion;
    }

    private void OnDisable()
    {
        _input.ConsumeHealthPotion -= ConsumeHealthPotion;
    }

    private void ConsumeHealthPotion()
    {
        if (_inventory.Consumable == null) return;
        Debug.Log("buscando slot consumible");
        if (_inventory.Consumable.slots.Count < 1) return;
        Debug.Log("corroborando que haya aunque sea 1");
        foreach (var slot in _inventory.Consumable.slots)
        {
            if (slot.IsEmpty) continue;
            Debug.Log("com 1");
            if (slot.stack.item == null) continue;
            Debug.Log("com 2");
            if (slot.stack.item.Type != ItemType.Consumable) continue;
            Debug.Log("com 3");
            if (slot.stack.item.EffectType != EffectType.Heal) continue;
            Debug.Log("com 4");
            _health.Heal(slot.stack.item.EffectAmount);
            Debug.Log("curando");
            _inventory.Consumable.ConsumeItem(slot.stack.item);
            Debug.Log("gastando");
            break;
        }
    }
}
