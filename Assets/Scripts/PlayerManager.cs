using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerMovement movement;
    private PlayerInventory inventory;

    private MeleeBehavior meleeBehavior;
    private NightVisionManager nightVisionManager;

    // the item we are holding currently
    private InventoryItem currentItem;

    void Start()
    {
        meleeBehavior = FindObjectOfType<MeleeBehavior>();
        nightVisionManager = FindObjectOfType<NightVisionManager>();
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();

        inventory.AddItem(new Melee());
        inventory.AddItem(new NightVision());

        currentItem = (Weapon)inventory.GetItem<Melee>();
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
                meleeBehavior.Attack((Melee)currentItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inventory.GetItem<NightVision>() != null)
            {
                nightVisionManager.Toggle();
            }
        }
    }
}
