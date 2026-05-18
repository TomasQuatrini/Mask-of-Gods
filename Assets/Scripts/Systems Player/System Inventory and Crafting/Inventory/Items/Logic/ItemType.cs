namespace System.Inventory
{
    public enum ItemType
    {
        Consumable, Equippable, Material, Quest, None
    }

    public enum ConsumableType
    {
        Health, Stamina, None
    }

    public enum EffectType
    {
        Heal, StaminaRestore, Damage, None
    }

    public enum WeaponAttackType
    {
        None, Melee, Ranged, Magic
    }
}