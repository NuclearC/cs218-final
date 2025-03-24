using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerMovement movement;
    private PlayerInventory inventory;

    private Weapon currentWeapon;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();

        inventory.AddItem(new Melee());

        currentWeapon = (Weapon)inventory.GetItem<Melee>();
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateInfoText(0, 0,
            currentWeapon != null ? currentWeapon.GetMagazineCapacity() : 0,
            currentWeapon != null ? currentWeapon.GetName() : "none",
            inventory.EnumerateItems().Select(i => i.GetName()).ToArray());

        if (Input.GetButton("Fire1"))
        {
            if (currentWeapon != null)
            {
                var obj = FindObjectOfType<MeleeBehavior>();
                obj.Attack((Melee)currentWeapon);
            }
        }
    }
}
