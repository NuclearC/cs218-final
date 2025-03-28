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

    private FirstPersonCamera firstPersonCamera;

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

        firstPersonCamera = Camera.main.GetComponent<FirstPersonCamera>();

        EquipItem(new Melee());
        EquipItem(new NightVision());

        var wtf = new Rifle
        {
            CurrentAmmo = 10
        };
        EquipItem(wtf);

        SetCurrentItem(inventory.GetItem<Melee>());
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
        UIManager.Instance.UpdateInfoText(0, currentItem is Rifle ? ((Rifle)currentItem).CurrentAmmo : 0,
            currentItem is Weapon ? ((Weapon)currentItem).GetMagazineCapacity() : 0,
            currentItem != null ? currentItem.GetName() : "none",
            inventory.EnumerateItems().Select(i => i.GetName()).ToArray());

        if (Input.GetButton("Fire1"))
        {
            if (currentItem is Weapon)
            {
                var obj = inventory.GetInventoryHudObject(currentItem.GetInventoryHudIndex());
                if (obj)
                {
                    var weaponBehavior = obj.GetComponent<WeaponBehavior>();
                    if (weaponBehavior && weaponBehavior.CanAttack())
                    {
                        weaponBehavior.Attack(currentItem as Weapon,
                            Quaternion.Euler(firstPersonCamera.GetViewAngles()) * Vector3.forward,
                            transform.position);
                    }
                }
            }
        }


        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                int idx = i - (int)KeyCode.Alpha1;
                var item = inventory.EnumerateItems().Where(it => it.GetInventoryHudIndex() == idx);
                if (item.Count() > 0)
                    SetCurrentItem(item.First());
            }
        }
    }
}
