
using System;
using UnityEngine;

public class Melee : Weapon
{

    private float hitRate = 0.5f;


    public override int GetInventoryHudIndex()
    {
        return PlayerInventory.INVENTORY_MELEE_INDEX;
    }

    public override string GetName()
    {
        return "Melee";
    }
    public override int GetAmount()
    {
        return 1;
    }

    public override int GetMagazineCapacity()
    {
        return 1;
    }

    public override float GetRange()
    {
        return 1.2f;
    }
    public override WeaponType GetWeaponType()
    {
        return WeaponType.Melee;
    }

    public override void AddAmmo(int count)
    {
    }

    public virtual void Attack(Vector3 origin, Vector3 direction)
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
        }
    }
}
