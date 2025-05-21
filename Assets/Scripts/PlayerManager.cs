using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerMovement movement;
    private PlayerInventory inventory;

    // the item we are holding currently
    private InventoryItem currentItem;

    private FirstPersonCamera firstPersonCamera;

    private HealthManager health;

    public PlayerMovement Movement { get { return movement; } }
    public PlayerInventory Inventory { get { return inventory; } }

    [SerializeField] GameObject testFragGrenade;
    [SerializeField] float useRadius = 2.0f;
    [SerializeField] float hintRadius = 5.0f;
    [SerializeField] AudioSource heartBeat;

    private bool secondAttack = false;

    private bool isDead = false;

    [SerializeField] bool loadState = false;

    public bool IsDead { get { return isDead; } }

    private float heartBeatPlayTime = 0.0f;


    private static PlayerManager localPlayerManager = null;
    public static PlayerManager GetLocalPlayerManager()
    {
        return localPlayerManager == null ? (localPlayerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>()) : localPlayerManager;
    }

    private void HeartBeatSound()
    {
        float elapsed = Time.time - heartBeatPlayTime;
        if (elapsed > 10.0f && heartBeat.isPlaying)
        {
            heartBeat.Stop();
        }
        else if (elapsed > 5.0f && heartBeat.isPlaying)
        {
            float volume = (10.0f - elapsed) / 5.0f;
            heartBeat.volume = volume;
        }
    }
    public void OnHealthChange(int newHealth, int delta)
    {
        if (delta < 0 && !isDead)
        {
            SoundManager.GetSoundManager().PlayImpactSound(transform.position);
            UIManager.Instance.FlashDamageIndicator();
            heartBeat.volume = 1.0f;
            heartBeat.Play();
            heartBeatPlayTime = Time.time;
        }

        if (newHealth <= 0)
        {
            heartBeat.Stop();
            OnDie();
        }
    }

    void Start()
    {
        health = GetComponent<HealthManager>();
        health.OnHealthChange.AddListener(OnHealthChange);

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
        if (loadState)
            LoadState();
        else ResetState();
    }

    public void LoadState()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            health.SetHealth(PlayerPrefs.GetInt("PlayerHealth"));
        }
        if (PlayerPrefs.HasKey("PlayerAmmo"))
        {
            var rifle = inventory.GetItem<Rifle>() as Rifle;
            rifle.TotalAmmo = PlayerPrefs.GetInt("PlayerAmmo");
            rifle.Reload();
        }
    }

    public void SaveState()
    {
        var rifle = inventory.GetItem<Rifle>() as Rifle;
        PlayerPrefs.SetInt("PlayerHealth", health.Value);
        PlayerPrefs.SetInt("PlayerAmmo", rifle.CurrentAmmo + rifle.TotalAmmo);
    }

    public void ResetState()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerAmmo");
    }

    public void OnDie()
    {
        ResetState();
        GameManager.Instance.CursorUnlock();
        movement.Freeze = true;
        var ui = UIManager.Instance;
        ui.ShowMenu("You died");
        ui.ShowDamageIndicator();
        firstPersonCamera.Locked = true;
        isDead = true;
        inventory.HideHUD();
    }
    public void EquipItem(InventoryItem item)
    {
        var soundManager = SoundManager.GetSoundManager();
        soundManager.PlayEquipSound(transform.position);

        inventory.AddItem(item);
    }

    public void SetCurrentItem(InventoryItem item)
    {
        if (item == null) return;
        inventory.SetActiveItem(item);

        currentItem = item;
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

        var colliders = Physics.OverlapSphere(transform.position, hintRadius);
        float angle = 0.0f;
        float dist = 0.0f;
        bool found = false;
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                if (col.TryGetComponent<UsableBehavior>(out var usable))
                {
                    var d = usable.transform.position - transform.position;
                    dist = d.magnitude;
                    var d2 = Camera.main.transform.rotation * Vector3.right;

                    var d3 = Camera.main.transform.rotation * Vector3.forward;

                    var dv = Vector3.Dot(d, d2);
                    float raw = Mathf.Acos(dv / (d.magnitude * d2.magnitude));

                    if (Mathf.Sign(Vector3.Dot(d, d3)) < 0)
                    {
                        raw = 2 * Mathf.PI - raw;
                    }

                    angle = Mathf.Rad2Deg * raw;
                    found = true;
                }
            }
        }
        if (found && (Math.Abs(angle - 90.0f) > 20.0f || dist > hintRadius / 2))
        {
            uiManager.ShowArrow(angle);
        }
        else uiManager.HideArrow();

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
        if (health.Value <= 0) return;
        if (GameManager.Instance.IsPaused) return;
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

        if (false && Input.GetKeyDown(KeyCode.G))
        {
            var obj = Instantiate(testFragGrenade, transform.position + firstPersonCamera.GetViewDirection(), Quaternion.LookRotation(firstPersonCamera.GetViewDirection()));

            obj.GetComponentInChildren<Rigidbody>().AddForce(firstPersonCamera.GetViewDirection() * 2 + Vector3.up * 3, ForceMode.VelocityChange);
            obj.GetComponentInChildren<Rigidbody>().AddRelativeTorque(Vector3.right + Vector3.up);
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

        if (inputHandler.Escape)
        {
            GameManager.Instance.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory.CurrentItem.GetInventoryHudIndex() == PlayerInventory.INVENTORY_NIGHTVISION_INDEX)
            {
                SetCurrentItem(inventory.FindWeapon(WeaponType.Primary));
            }
            else
            {
                var item = inventory.EnumerateItems().Where(it => it.GetInventoryHudIndex() == PlayerInventory.INVENTORY_NIGHTVISION_INDEX);
                if (item.Count() > 0)
                    SetCurrentItem(item.First());
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
        UIManager.Instance.UpdateInventory(currentItem.GetInventoryHudIndex());

        UIManager.Instance.UpdateInfoText(health.Value, currentItem is Rifle ? ((Rifle)currentItem).CurrentAmmo : 0,
            currentItem is Rifle ? ((Rifle)currentItem).GetMagazineCapacity() : 1,
            currentItem is Rifle ? ((Rifle)currentItem).TotalAmmo : 1,
            currentItem != null ? currentItem.GetName() : "none",
            inventory.EnumerateItems().Select(i => i.GetName()).ToArray());

        CheckFOVItems();
        HeartBeatSound();
    }

    void LateUpdate()
    {
        ProcessInput();
    }
}
