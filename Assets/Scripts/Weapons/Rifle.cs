
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
public class Rifle : Weapon
{
    public int CurrentAmmo { get; set; }
    public int TotalAmmo { get; set; }
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

    public void Reload()
    {
        int bag = TotalAmmo + CurrentAmmo;
        int mi = Math.Min(bag, GetMagazineCapacity());
        TotalAmmo = bag - mi;
        CurrentAmmo = mi;
    }
    public override void AddAmmo(int count)
    {
        TotalAmmo += count;
    }

    public void FireBullet(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);

        var hits = Physics.RaycastAll(ray, GetRange());
        // sort by distance
        Array.Sort(hits, (left, right) => { return left.distance.CompareTo(right.distance); });

        for (int i = 0; i < hits.Length; i++)
        {
            var hitInfo = hits[i];

            var other = hitInfo.collider.gameObject;
            var hittable = other.GetComponent<HittableBehavior>();

            if (hittable)
            {
                if (hittable.OnBulletImpact(direction, hitInfo.distance, hitInfo.point, hitInfo.normal))
                    break;
            }
            BulletImpact.OnImpact(other, direction, hitInfo.point, hitInfo.normal);
        }
    }
}