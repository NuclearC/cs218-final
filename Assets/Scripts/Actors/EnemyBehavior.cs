using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float bloodDecalDistance = 2.0f;

    [SerializeField] Transform[] patrolWaypoints;

    private Animator animator;
    private HealthManager health;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    private bool isDead = false;

    private float lastSetDestTime = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();

        health.OnHealthChange.AddListener((int newHealth, int change) =>
        {
            print("enemy health change " + newHealth);
            if (isDead == false && newHealth <= 0)
            {
                isDead = true;
                animator.SetBool("dead", true);

                EventManager.Instance.OnEnemyDie.Invoke();
            }
        });
    }
    public void OnBulletImpact(Vector3 direction, Vector3 hitPoint, Vector3 hitNormal)
    {
        var soundManager = SoundManager.GetSoundManager();
        soundManager.PlayImpactSound(hitPoint);

        var fxManager = FXManager.GetEffectsManager();

        fxManager.OnBloodImpact(gameObject, hitPoint, direction);

        if (isDead == false)
            health.AddHealth(-20);

        // project blood decal if there is something behind
        if (Physics.Raycast(new Ray(hitPoint, direction), out var hitInfo, bloodDecalDistance, ~(1 << LayerMask.NameToLayer("Enemies"))))
        {
            var decalManager = DecalManager.GetDecalManager();
            decalManager.CreateBloodDecal(hitInfo.collider.gameObject, hitInfo.point + hitInfo.normal * 0.05f, -hitInfo.normal);
        }
        // project blood decal if there is something downwards
        if (Physics.Raycast(new Ray(hitPoint, Vector3.down), out hitInfo, bloodDecalDistance, ~(1 << LayerMask.NameToLayer("Enemies"))))
        {
            var decalManager = DecalManager.GetDecalManager();
            decalManager.CreateBloodDecal(hitInfo.collider.gameObject, hitInfo.point + hitInfo.normal * 0.05f, -hitInfo.normal);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            float distance = Vector3.Distance(transform.position, patrolWaypoints[currentWaypoint].position);
            if (distance <= 1.0f)
            {
                currentWaypoint = (currentWaypoint + 1) % patrolWaypoints.Count();
            }
            else
            {
                if (Time.time - lastSetDestTime >= 5.0f)
                {
                    agent.SetDestination(patrolWaypoints[currentWaypoint].position);
                    lastSetDestTime = Time.time;
                }
            }
            animator.SetFloat("moveSpeed", agent.velocity.magnitude);
        }
    }
}
