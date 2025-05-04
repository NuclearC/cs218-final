using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float bloodDecalDistance = 2.0f;

    [SerializeField] Transform[] patrolWaypoints;
    [SerializeField] int damage = 16;

    private Animator animator;
    private HealthManager health;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    private bool isDead = false;

    private float lastSetDestTime = 0;

    private float lastHitTime = 0;
    [SerializeField] float hitInterval = 0.5f;
    [SerializeField] float chaseDistance = 10.0f;


    [SerializeField] float chaseSpeed = 3.0f;
    [SerializeField] float patrolSpeed = 1.3f;
    [SerializeField] AudioClip[] footsteps;
    private float nextFootstepPlayTime = 0.0f;
    private Transform playerTransform;

    enum BehavioralState
    {
        Patrol,
        Chase,
        Wait,
    }

    private BehavioralState currentState;
    private float lastStateSwitch;

    private readonly float stateSwitchDelay = 4.0f;

    void Start()
    {

        currentState = BehavioralState.Patrol;
        lastStateSwitch = Time.time;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthManager>();

        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.GetComponent<Transform>();
        }

        health.OnHealthChange.AddListener((int newHealth, int change) =>
        {
            print("enemy health change " + newHealth);
            if (isDead == false && newHealth <= 0)
            {
                agent.enabled = false;
                isDead = true;
                animator.SetTrigger("deathTrigger");

                EventManager.Instance.OnEnemyDie.Invoke();
            }
        });
    }
    void OnCollisionStay(Collision other)
    {
        var col = other.collider;
        if (!isDead && col.CompareTag("Player") && col.TryGetComponent<HealthManager>(out var healthManager) && col.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            if (Time.time - lastHitTime > hitInterval)
            {
                lastHitTime = Time.time;
                healthManager.AddHealth(-damage);
                var contact = other.GetContact(0);
                rigidbody.AddForceAtPosition(contact.normal * (damage / 10) * 5.0f, contact.point, ForceMode.Impulse);
            }
        }
    }
    public void OnBulletImpact(Vector3 direction, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!isDead)
        {
            var soundManager = SoundManager.GetSoundManager();
            soundManager.PlayFleshImpactSound(hitPoint);
        }

        var fxManager = FXManager.GetEffectsManager();

        fxManager.OnBloodImpact(gameObject, hitPoint, direction);

        if (isDead == false)
            health.AddHealth(-20);

        currentState = BehavioralState.Chase;

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

    void Patrol()
    {
        agent.speed = patrolSpeed;

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
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(playerTransform.position);
    }
    bool CanSwitchState()
    {
        return Time.time - lastStateSwitch >= stateSwitchDelay;
    }

    void SetState(BehavioralState newState)
    {
        if (CanSwitchState())
        {
            lastStateSwitch = Time.time;
            currentState = newState;
        }
    }

    void UpdateState()
    {
        float playerDistance = float.PositiveInfinity;
        if (playerTransform)
        {
            playerDistance = Vector3.Distance(transform.position, playerTransform.position);
        }


        if (playerDistance < 10.0f)
        {
            var speed = agent.velocity.magnitude * 1.5f;

            if (speed > 1.0f && Time.time >= nextFootstepPlayTime)
            {
                AudioSource.PlayClipAtPoint(footsteps[Random.Range(0, footsteps.Length)], transform.position + Vector3.down * 2.0f);
                nextFootstepPlayTime = Time.time + 1.0f / speed;
            }
        }

        switch (currentState)
        {
            case BehavioralState.Patrol:
                if (patrolWaypoints.Length > 0)
                {
                    Patrol();
                    if (playerDistance < chaseDistance)
                        SetState(BehavioralState.Chase);
                    else
                        SetState(BehavioralState.Patrol);
                    break;
                }
                goto case BehavioralState.Chase;
            case BehavioralState.Chase:
                Chase();
                if (playerDistance >= chaseDistance)
                    SetState(BehavioralState.Wait);
                break;
            case BehavioralState.Wait:
                if (CanSwitchState())
                    SetState(BehavioralState.Patrol);
                break;
        }

        animator.SetFloat("moveSpeed", agent.velocity.magnitude);
    }

    void Update()
    {
        if (!isDead)
        {
            UpdateState();
        }
    }
}
