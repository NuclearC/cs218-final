using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
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

    [SerializeField] GameObject testFragGrenade;
    [SerializeField] float useRadius = 2.0f;

    private bool secondAttack = false;


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
        var soundManager = SoundManager.GetSoundManager();
        soundManager.PlayEquipSound(transform.position);

        inventory.AddItem(item);
    }

    public void SetCurrentItem(InventoryItem item)
    {
        inventory.SetActiveItem(item);

        currentItem = item;
    }
    void OnTriggerEnter(Collider other)
    {
    }


    // checks if there are any items in FOV
    void CheckFOVItems()
    {
        var uiManager = UIManager.Instance;
        bool uiShown = false;
        if (Physics.Raycast(transform.position, firstPersonCamera.GetViewDirection(),
                            out var hitInfo, useRadius))
        {
            if (hitInfo.collider.TryGetComponent<ItemBehavior>(out var itemBehavior))
            {
                uiShown = true;
                uiManager.ShowFloatingPanel(itemBehavior.DisplayName, Camera.main.WorldToScreenPoint(itemBehavior.transform.position));
            }
        }

        if (!uiShown)
        {
            uiManager.HideFloatingPanel();
        }
    }

    void Use()
    {
        if (Physics.Raycast(transform.position, firstPersonCamera.GetViewDirection(),
                            out var hitInfo, useRadius))
        {
            if (hitInfo.collider.TryGetComponent<UsableBehavior>(out var usableBehavior))
            {
                usableBehavior.OnUse(this);
            }
        }
    }

    void ProcessInput()
    {
        var inputHandler = GameManager.Instance.InputHandler;
        if (inputHandler.SecondaryAttack)
        {
            if (secondAttack == false)
            {
                if (currentItem is Weapon)
                {
                    var obj = inventory.GetInventoryHudObject(currentItem.GetInventoryHudIndex());
                    if (obj)
                    {
                        var weaponBehavior = obj.GetComponent<WeaponBehavior>();
                        if (weaponBehavior)
                        {
                            weaponBehavior.AttackSecondary(currentItem as Weapon);
                        }
                    }
                }
            }

            secondAttack = true;
        }
        else secondAttack = false;

        if (Input.GetKeyDown(KeyCode.E) && false)
        {
            var obj = Instantiate(testFragGrenade, transform.position, Quaternion.LookRotation(firstPersonCamera.GetViewDirection()));
        }

        if (inputHandler.UsePrimary)
        {
            Use();
        }

        if (inputHandler.PrimaryAttack)
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
                            firstPersonCamera.GetViewDirection(),
                            transform.position);
                    }
                }
            }
        }

        if (inputHandler.Reload)
        {
            if (currentItem is Rifle)
            {
                var obj = inventory.GetInventoryHudObject(currentItem.GetInventoryHudIndex());
                if (obj)
                {
                    if (obj.TryGetComponent<RifleBehavior>(out var rifleBehavior))
                    {
                        rifleBehavior.Reload(currentItem as Rifle);
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
    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateInfoText(0, currentItem is Rifle ? ((Rifle)currentItem).CurrentAmmo : 0,
            currentItem is Rifle ? ((Rifle)currentItem).TotalAmmo : 1,
            currentItem != null ? currentItem.GetName() : "none",
            inventory.EnumerateItems().Select(i => i.GetName()).ToArray());

        CheckFOVItems();
    }

    void LateUpdate()
    {
        ProcessInput();
    }
}
