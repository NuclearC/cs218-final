using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBehavior : WeaponBehavior
{
    private Animator animator;
    private bool canAttack;
    private float nextAttackTime = 0.0f;
    public override void Attack(Weapon weapon, Vector3 viewDirection, Vector3 attackOrigin)
    {
        if (CanAttack() && weapon is Rifle)
        {
            Rifle rifle = weapon as Rifle;
            if (rifle.CurrentAmmo <= 0)
                return;

            animator.SetTrigger("fireTrigger");
            canAttack = false;

            rifle.FireBullet(attackOrigin, viewDirection);

            rifle.CurrentAmmo -= 1;

            nextAttackTime = Time.time + 60.0f / 666.0f;
        }
    }

    public override bool CanAttack()
    {
        return canAttack && Time.time >= nextAttackTime;
    }

    private List<Vector3> hits = new();
    void OnDrawGizmos()
    {
        foreach (var hit in hits)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(hit, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }

    public void OnReady()
    {
        canAttack = true;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
