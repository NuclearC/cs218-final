using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : UsableBehavior
{
    [SerializeField] protected string itemDisplayName;

    protected InventoryItem inventoryItem;

    public string DisplayName { get { return itemDisplayName; } }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnUse(PlayerManager playerManager)
    {
        playerManager.EquipItem(inventoryItem);
        Destroy(gameObject);
    }
}
