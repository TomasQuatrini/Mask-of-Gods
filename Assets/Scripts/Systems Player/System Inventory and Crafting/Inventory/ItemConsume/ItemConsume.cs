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
        _input.ConsumeStaminaPotion += ConsumeStaminaPotion;
    }

    private void OnDisable()
    {
        _input.ConsumeHealthPotion -= ConsumeHealthPotion;
        _input.ConsumeStaminaPotion -= ConsumeStaminaPotion;
    }

    private void ConsumeHealthPotion()
    {
        if (_inventory.GetInventory(ItemType.Consumable) == null) return;
        if (_inventory.GetInventory(ItemType.Consumable).slots.Count < 1) return;
        foreach (var slot in _inventory.GetInventory(ItemType.Consumable).slots)
        {
            if (slot.IsEmpty) continue;
            if (slot.stack.item == null) continue;
            if (slot.stack.item.Type != ItemType.Consumable) continue;
            if (slot.stack.item.EffectType != EffectType.Heal) continue;

            var TryConsume = _health.Heal(slot.stack.item.EffectAmount);
            if (TryConsume is true)
            {
                _inventory.ConsumeItem(slot.stack.item, 1);
            }
            break;
        }
    }

    private void ConsumeStaminaPotion()
    {
        if (_inventory.GetInventory(ItemType.Consumable) == null) { return; }
        if (_inventory.GetInventory(ItemType.Consumable).slots.Count < 1) return;
        foreach (var slot in _inventory.GetInventory(ItemType.Consumable).slots)
        {
            if (slot.IsEmpty) continue;
            if (slot.stack.item == null) continue;
            if (slot.stack.item.Type != ItemType.Consumable) continue;
            if (slot.stack.item.EffectType != EffectType.StaminaRestore) continue;
            var TryConsume = _stamina.TryRestore(slot.stack.item.EffectAmount); 
            if (TryConsume is true)
            {
                _inventory.ConsumeItem(slot.stack.item, 1);
            }
            break;
        }
    }

}