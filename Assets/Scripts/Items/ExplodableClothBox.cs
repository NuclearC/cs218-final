using UnityEngine;

public class ExplodableClothBox : HittableBehavior
{
    [SerializeField] GameObject explosionEffectPrefab; // Optional explosion VFX
    [SerializeField] float horizontalForce = 50; // Force to push sideways
    [SerializeField] float upwardForce = 200; // Small upward force
    [SerializeField] float spinTorque = 200f; // Torque for spinning
    [SerializeField] float explosionRadius = 5f; // Radius for explosion force
    [SerializeField] float destroyVfxDelay = 5f;

    private bool exploded = false;

    public override bool OnBulletImpact(Vector3 direction, float distance, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (exploded)
            return false;

        exploded = true;

        // Spawn explosion effect at impact point
        if (explosionEffectPrefab)
        {
            GameObject vfx = Instantiate(explosionEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(vfx, destroyVfxDelay);
        }

        // Add Rigidbody if missing
        if (!TryGetComponent<Rigidbody>(out var rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Activate physics
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Calculate big horizontal explosion
        Vector3 explosionDirection = new Vector3(
            Random.Range(-1f, 1f), // random left or right
            0.5f, // slight upward
            Random.Range(-1f, 1f) // random forward/backward
        ).normalized;

        Vector3 force = explosionDirection * horizontalForce + Vector3.up * upwardForce;

        rb.AddForce(force, ForceMode.Impulse);

        // Add spinning during flight
        rb.AddTorque(Random.insideUnitSphere * spinTorque, ForceMode.Impulse);

        Debug.Log("[ClothBox] Launched into the air!");

        return true;
    }
}
