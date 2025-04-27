using UnityEngine;

public class DestructiblePowerBox : HittableBehavior
{
    [SerializeField] GameObject smallFireEffectPrefab; // Fire VFX when shot
    [SerializeField] GameObject bigExplosionEffectPrefab; // Explosion VFX after destroyed
    [SerializeField] Material destroyedMaterial;  // Broken/destroyed material
    [SerializeField] float smallVfxDuration = 3f; // How long small fire lasts
    [SerializeField] float bigVfxDuration = 10f;  // How long big explosion fire lasts
    [SerializeField] int hitsToDestroy = 3;        // How many hits needed

    private int currentHits = 0;
    private bool isDestroyed = false;
    private Renderer boxRenderer;
    private Material originalMaterial;

    void Start()
    {
        boxRenderer = GetComponent<Renderer>();
        if (boxRenderer)
        {
            originalMaterial = boxRenderer.material;
        }
    }

    public override bool OnBulletImpact(Vector3 direction, float distance, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isDestroyed)
            return false;

        currentHits++;
        Debug.Log($"[PowerBox] Hit {currentHits}/{hitsToDestroy}");

        // Spawn small fire effect at hit location
        if (smallFireEffectPrefab)
        {
            Vector3 fireSpawnPos = hitPoint + Vector3.up * 0.2f;
            GameObject vfx = Instantiate(smallFireEffectPrefab, fireSpawnPos, Quaternion.identity);
            Destroy(vfx, smallVfxDuration);
        }

        if (currentHits >= hitsToDestroy)
        {
            DestroyPowerBox();
        }

        return true;
    }

    private void DestroyPowerBox()
    {
        isDestroyed = true;

        // Change material to destroyed version
        if (destroyedMaterial && boxRenderer)
        {
            boxRenderer.material = destroyedMaterial;
        }

        // Spawn big explosion/fire effect at center
        if (bigExplosionEffectPrefab)
        {
            Vector3 boxCenter = transform.position + Vector3.up * 1f; // Adjust height if needed
            GameObject bigVfx = Instantiate(bigExplosionEffectPrefab, boxCenter, Quaternion.identity);
            Destroy(bigVfx, bigVfxDuration);
        }

        Debug.Log("[PowerBox] DESTROYED!");
    }
}
