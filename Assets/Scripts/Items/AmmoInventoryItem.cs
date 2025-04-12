
public class AmmoInventoryItem : InventoryItem
{
    public override int GetAmount()
    {
        return 30;
    }

    public override string GetName()
    {
        return "Ammo";
    }
    public override bool OnEquipped(PlayerInventory inventory)
    {
        Weapon weapon = inventory.FindWeapon(WeaponType.Primary);
        if (weapon != null)
        {
            weapon.AddAmmo(GetAmount());
        }

        return false;
    }
}