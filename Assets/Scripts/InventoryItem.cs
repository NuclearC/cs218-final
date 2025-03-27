

using UnityEngine;

public abstract class InventoryItem
{
    public virtual string GetObjectName()
    {
        return "";
    }
    public abstract string GetName();
    public abstract int GetAmount();
}
