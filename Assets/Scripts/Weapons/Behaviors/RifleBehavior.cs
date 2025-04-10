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
        float y = UnityEngine.Random.Range(-10.0f, 10.0f);
        float x = UnityEngine.Random.Range(-10.0f, 10.0f);
        float factor = 1.0f;
        if (isScopedIn)
        {
            float elapsed = Time.time - nextAttackTime;
            factor = (1.0f - MathF.Min(1.0f, elapsed)) / 2.0f;
        }
        return new Vector2(x, y) * factor;
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
            var soundManager = SoundManager.GetSoundManager();

            Rifle rifle = weapon as Rifle;
            if (rifle.CurrentAmmo <= 0)
                return;

            soundManager.OnRifleFire();

            animator.SetTrigger("fireTrigger");
            canAttack = false;

            muzzleEffect.Play();

            var spread = CalculateSpread();

            rifle.FireBullet(attackOrigin, Quaternion.Euler(spread.x, spread.y, 0.0f) * viewDirection);

            firstPersonCamera.AddRecoil(isScopedIn ? -2.0f : -5.0f, spread.y * UnityEngine.Random.Range(0.5f, 1.0f));

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
