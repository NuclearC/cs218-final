
using UnityEngine;

public class Melee : Weapon
{

    private float hitRate = 0.5f;


    public override int GetInventoryHudIndex()
    {
        return PlayerInventory.INVENTORY_MELEE_INDEX;
    }

    public override string GetName()
    {
        return "Melee";
    }
    public override int GetAmount()
    {
        return 1;
    }

    public override int GetMagazineCapacity()
    {
        return 1;
    }

    public override float GetRange()
    {
        return 50.0f;
    }
    public override WeaponType GetWeaponType()
    {
        return WeaponType.Melee;
    }

    public override void AddAmmo(int count)
    {
    }
}
