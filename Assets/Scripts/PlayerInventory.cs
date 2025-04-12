using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static int INVENTORY_RIFLE_INDEX = 0;
    public static int INVENTORY_MELEE_INDEX = 2;
    public static int INVENTORY_NIGHTVISION_INDEX = 3;

    private List<InventoryItem> items = new();

    public InventoryItem CurrentItem { get; private set; }

    [SerializeField] GameObject[] inventoryHudObjects;

    public void AddItem(InventoryItem item)
    {
        if (item.OnEquipped(this))
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

    public GameObject GetInventoryHudObject(int index)
    {
        if (index >= 0 && index < inventoryHudObjects.Count())
            return inventoryHudObjects[index];
        return null;
    }

    public Weapon FindWeapon(WeaponType type)
    {
        return items.Find(i => i is Weapon && (i as Weapon).GetWeaponType() == type) as Weapon;
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
