using UnityEngine;

public class DestructibleFan : HittableBehavior
{
    [SerializeField] GameObject explosionEffectPrefab; // VFX_Fire_01_Big_Simple
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionTorque = 50f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float destroyVFXDelay = 5f;

    private bool exploded = false;

    public override bool OnBulletImpact(Vector3 direction, float distance, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (exploded)
            return false;

        exploded = true;

        // Spawn fire effect slightly above
        Vector3 fireSpawnPosition = transform.position + Vector3.up * 0.5f;
        if (explosionEffectPrefab)
        {
            GameObject vfx = Instantiate(explosionEffectPrefab, fireSpawnPosition, Quaternion.identity);
            Destroy(vfx, destroyVFXDelay); // VFX disappears after a while
        }

        // Add Rigidbody if missing
        if (!TryGetComponent<Rigidbody>(out var rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Make sure the MeshCollider is convex
        if (TryGetComponent<MeshCollider>(out var meshCollider))
        {
            meshCollider.convex = true;
        }

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Apply explosion physics
        rb.AddExplosionForce(explosionForce, transform.position - direction, explosionRadius);
        rb.AddTorque(Random.insideUnitSphere * explosionTorque, ForceMode.Impulse);

        return true;
    }
}
