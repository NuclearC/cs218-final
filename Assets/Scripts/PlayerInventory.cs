using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> items = new();
    [SerializeField] GameObject[] inventoryItemGameObjects;

    void Start()
    {

    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    public void SetActiveItem(InventoryItem item)
    {
        foreach (var gameObject in inventoryItemGameObjects)
        {
            if (gameObject.name == item.GetObjectName())
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
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
