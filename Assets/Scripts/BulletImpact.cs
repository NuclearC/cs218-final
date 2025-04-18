
using UnityEngine;

public class BulletImpact
{
    public static void OnImpact(GameObject gameObject, Vector3 direction, Vector3 hitPoint, Vector3 hitNormal)
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForceAtPosition(direction * 10.0f, hitPoint, ForceMode.Impulse);
        }
        var fxManager = FXManager.GetEffectsManager();
        var decalManager = DecalManager.GetDecalManager();

        fxManager.OnImpactSmoke(gameObject, hitPoint, hitNormal);
        fxManager.OnImpact(gameObject, hitPoint, Vector3.Reflect(direction, hitNormal));
        decalManager.CreateDecal(gameObject, hitPoint + hitNormal * 0.05f, -hitNormal);
    }
}
