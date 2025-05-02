
using UnityEngine;

public class MeleeBehavior : WeaponBehavior
{
    private Animator animator;
    private bool canAttack = false;

    private Melee melee;
    private Vector3 attackOrigin, attackDirection;


    public void OnHit()
    {
        melee.Attack(attackOrigin, attackDirection);
    }
    public void OnReady()
    {
        print("ready");
        canAttack = true;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override bool CanAttack()
    {
        return canAttack;
    }
    public override void AttackSecondary(Weapon weapon)
    {
    }
    public override void Attack(Weapon weapon, Vector3 viewDirection, Vector3 attackOrigin)
    {
        if (CanAttack())
        {
            melee = weapon as Melee;
            attackDirection = viewDirection;
            this.attackOrigin = attackOrigin;
            animator.SetTrigger("HitTrigger");
            canAttack = false;
        }
    }

    public string GetInventoryItemName()
    {
        return "Melee";
    }
}
