using UnityEngine;

public class NightVision : InventoryItem
{
    public override int GetAmount()
    {
        return 1;
    }

    public override int GetInventoryHudIndex()
    {
        return PlayerInventory.INVENTORY_NIGHTVISION_INDEX;
    }

    public override string GetName()
    {
        return "Night Vision";
    }
}