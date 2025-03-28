using UnityEngine;

public class NightVision : InventoryItem
{
    public override int GetAmount()
    {
        return 1;
    }

    public override int GetInventoryHudIndex()
    {
        return 1;
    }

    public override string GetName()
    {
        return "Night Vision";
    }
    public override string GetObjectName()
    {
        return "NVScreen";
    }
}