using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> items = new();

    public InventoryItem CurrentItem { get; private set; }

    [SerializeField] GameObject[] inventoryHudObjects;

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    public void SetActiveItem(InventoryItem item)
    {
        CurrentItem = item;
        if (item.GetInventoryHudIndex() == -1)
            return;

        for (int i = 0; i < inventoryHudObjects.Count(); i++)
        {
            if (i == CurrentItem.GetInventoryHudIndex())
                inventoryHudObjects[i].SetActive(true);
            else inventoryHudObjects[i].SetActive(false);
        }
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
