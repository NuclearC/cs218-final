using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MeleeBehavior : MonoBehaviour, WeaponBehavior<Melee>
{
    private Animator animator;

    private bool canAttack = false;
    public void Attack(Melee weapon)
    {
        if (CanAttack())
        {
            animator.SetTrigger("HitTrigger");
            canAttack = false;
        }
    }

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

    public bool CanAttack()
    {
        return canAttack;
    }
}
