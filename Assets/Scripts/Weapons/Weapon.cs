

using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary,
    Melee,
    Utility,
}
public abstract class Weapon : InventoryItem
{
    public override string GetName()
    {
        return "Weapon";
    }

    public abstract int GetMagazineCapacity();

    // get the range of this weapon in Unity units
    public abstract float GetRange();

    public abstract WeaponType GetWeaponType();

    public virtual void Attack()
    {
    }
}
