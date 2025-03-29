
using UnityEngine;
public abstract class WeaponBehavior : MonoBehaviour
{
    public abstract void Attack(Weapon weapon, Vector3 viewDirection, Vector3 attackOrigin);

    public abstract void AttackSecondary(Weapon weapon);

    public abstract bool CanAttack();
}