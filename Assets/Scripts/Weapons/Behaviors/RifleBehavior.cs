using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class RifleBehavior : WeaponBehavior
{
    private Animator animator;
    private FirstPersonCamera firstPersonCamera;

    [SerializeField] ParticleSystem muzzleEffect;
    private bool canAttack;
    private float nextAttackTime = 0.0f;

    private bool isScopedIn = false;

    private Vector2 CalculateSpread()
    {
        if (isScopedIn)
            return Vector2.zero;
        // this shit's mostly random
        float y = UnityEngine.Random.Range(-10.0f, 10.0f);
        float x = UnityEngine.Random.Range(-10.0f, 10.0f);
        return new Vector2(x, y);
    }
    public override void AttackSecondary(Weapon weapon)
    {
        isScopedIn = !animator.GetBool("scopedIn");

        animator.SetBool("scopedIn", isScopedIn);
    }
    public override void Attack(Weapon weapon, Vector3 viewDirection, Vector3 attackOrigin)
    {
        if (CanAttack() && weapon is Rifle)
        {
            Rifle rifle = weapon as Rifle;
            if (rifle.CurrentAmmo <= 0)
                return;

            animator.SetTrigger("fireTrigger");
            canAttack = false;

            muzzleEffect.Play();

            var spread = CalculateSpread();

            rifle.FireBullet(attackOrigin, Quaternion.Euler(spread.x, spread.y, 0.0f) * viewDirection);

            firstPersonCamera.AddRecoil(-5.0f, spread.y);

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
        firstPersonCamera = Camera.main.GetComponent<FirstPersonCamera>();
    }
}
