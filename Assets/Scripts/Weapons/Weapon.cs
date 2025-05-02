

using System;
using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary,
    Melee,
    Utility,
}
public abstract class Weapon : InventoryItem
{
    public override string GetName()
    {
        return "Weapon";
    }

    public abstract int GetMagazineCapacity();

    // get the range of this weapon in Unity units
    public abstract float GetRange();

    public abstract void AddAmmo(int count);

    public abstract WeaponType GetWeaponType();

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
            BulletImpact.OnImpact(other, direction, hitInfo.point, hitInfo.normal);
        }
    }
}
