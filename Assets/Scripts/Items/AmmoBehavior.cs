using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehavior : ItemBehavior
{
    void Awake()
    {
        itemDisplayName = "Ammo";
        inventoryItem = new AmmoInventoryItem();
    }
}
