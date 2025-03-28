using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerMovement movement;
    private PlayerInventory inventory;

    // the item we are holding currently
    private InventoryItem currentItem;

    public PlayerMovement Movement { get { return movement; } }
    public PlayerInventory Inventory { get { return inventory; } }


    private static PlayerManager localPlayerManager = null;
    public static PlayerManager GetLocalPlayerManager()
    {
        return localPlayerManager == null ? (localPlayerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>()) : localPlayerManager;
    }

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();

        EquipItem(new Melee());
        EquipItem(new NightVision());
    }

    public void EquipItem(InventoryItem item)
    {
        inventory.AddItem(item);
    }

    public void SetCurrentItem(InventoryItem item)
    {
        inventory.SetActiveItem(item);

        currentItem = item;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateInfoText(0, 0,
            currentItem is Weapon ? ((Weapon)currentItem).GetMagazineCapacity() : 0,
            currentItem != null ? currentItem.GetName() : "none",
            inventory.EnumerateItems().Select(i => i.GetName()).ToArray());

        if (Input.GetButton("Fire1"))
        {
            if (currentItem is Weapon)
            {
                //meleeBehavior.Attack((Melee)currentItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (nightVisionManager != null && inventory.GetItem<NightVision>() != null)
            //{
            //    nightVisionManager.Toggle();
            //}
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCurrentItem(inventory.EnumerateItems().ElementAt(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurrentItem(inventory.EnumerateItems().ElementAt(1));
        }
    }
}
