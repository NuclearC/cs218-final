using UnityEngine;

public class ExplodableBarrel : HittableBehavior
{
    [SerializeField] GameObject explosionEffectPrefab; // VFX_Fire_01_Big_Simple
    [SerializeField] GameObject explodedBarrelPrefab;   // Oil Barrel-Exploded
    [SerializeField] float destroyDelay = 5f;

    private bool exploded = false;


    public override bool OnBulletImpact(Vector3 direction, float distance, Vector3 hitPoint, Vector3 hitNormal)
        {
            if (exploded) return false;
            exploded = true;

            // Raise fire effect
            Vector3 fireSpawnPosition = transform.position + Vector3.up * 0.5f;
            if (explosionEffectPrefab)
            {
                GameObject vfx = Instantiate(explosionEffectPrefab, fireSpawnPosition, Quaternion.identity);
                Destroy(vfx, 5f);
            }

            // Small bounce/jump
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(Vector3.up * 15f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 20f, ForceMode.Impulse);
            }

            // Spawn exploded version
            if (explodedBarrelPrefab)
            {
                Instantiate(explodedBarrelPrefab, transform.position, transform.rotation);
            }

            // Destroy original
            Destroy(gameObject, 0.1f); // slight delay to allow bounce before disappearing

            return true;
        }


}
