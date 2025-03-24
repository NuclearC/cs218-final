using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> items = new();

    void Start()
    {

    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    public InventoryItem GetItem<T>()
    {
        return items.Find(i => i is T);
    }

    public IEnumerable<InventoryItem> EnumerateItems()
    {
        foreach (var i in items)
            yield return i;
    }
}
