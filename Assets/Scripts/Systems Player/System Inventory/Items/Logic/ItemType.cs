namespace System.Inventory
{
    public enum ItemType
    {
        Consumable, Equippable, Mask
    }

    public enum ConsumableType
    {
        Health, Stamina
    }

    public enum EffectType
    {
        Heal, StaminaRestore, Damage
    }

    public enum WeaponAttackType
    {
        None , Melee, Ranged, Magic
    }
}