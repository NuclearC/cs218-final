
using UnityEngine;
public class Rifle : Weapon
{
    public int CurrentAmmo { get; set; }
    public override int GetInventoryHudIndex()
    {
        return PlayerInventory.INVENTORY_RIFLE_INDEX;
    }

    public override string GetName()
    {
        return "Rifle";
    }
    public override int GetAmount()
    {
        return 1;
    }
    public override int GetMagazineCapacity()
    {
        return 30;
    }

    public override float GetRange()
    {
        return 50.0f;
    }
    public override WeaponType GetWeaponType()
    {
        return WeaponType.Primary;
    }

    public void FireBullet(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out var hitInfo, GetRange()))
        {
            var rb = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForceAtPosition(direction * 10.0f, hitInfo.point, ForceMode.Impulse);
            }

            var particlesManager = ParticlesManager.GetParticlesManager();
            particlesManager.Emit(hitInfo.point, hitInfo.normal);

            Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red, 5.0F);
        }
    }
}