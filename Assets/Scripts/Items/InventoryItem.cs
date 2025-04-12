

using UnityEngine;

public abstract class InventoryItem
{
    public abstract string GetName();
    public abstract int GetAmount();

    public virtual bool OnEquipped(PlayerInventory inventory) { return true; }

    public virtual int GetInventoryHudIndex()
    {
        return -1;
    }
}
