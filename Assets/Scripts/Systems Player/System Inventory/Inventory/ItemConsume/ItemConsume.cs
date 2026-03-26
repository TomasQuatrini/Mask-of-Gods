using UnityEngine;
using System.Inventory;

public class ItemConsume : MonoBehaviour
{
    private PlayerInventoryComponent _inventory;
    private PlayerContext _ctx;
    private PlayerHealth _health;
    private PlayerStaminaSM _stamina;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _inventory = _ctx.Inventory;
        _health = _ctx.Health;
        _stamina = _ctx.Stamina;
    }

    public void ConsumeItem()
    {

    }
}
