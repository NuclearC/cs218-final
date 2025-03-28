
using UnityEngine;

public class MeleeBehavior : WeaponBehavior
{
    private Animator animator;
    private bool canAttack = false;


    public void OnHit()
    {
        Debug.Log("melee hit");
    }
    public void OnReady()
    {
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
    public override void Attack(Weapon weapon, Vector3 viewDirection, Vector3 attackOrigin)
    {
        if (CanAttack())
        {
            animator.SetTrigger("HitTrigger");
            canAttack = false;
        }
    }

    public string GetInventoryItemName()
    {
        return "Melee";
    }
}
