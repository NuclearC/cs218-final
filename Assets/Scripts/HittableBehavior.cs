using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableBehavior : MonoBehaviour
{
    public bool OnBulletImpact(Vector3 direction, float distance, Vector3 hitPoint, Vector3 hitNormal)
    {
        var enemyBehavior = GetComponent<EnemyBehavior>();
        if (enemyBehavior)
        {
            enemyBehavior.OnBulletImpact(direction, hitPoint, hitNormal);
            return false;
        }
        else
        {
            var fxManager = FXManager.GetEffectsManager();
            var decalManager = DecalManager.GetDecalManager();

            var rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForceAtPosition(direction * 10.0f, hitPoint, ForceMode.Impulse);
            }

            fxManager.OnImpactSmoke(gameObject, hitPoint, hitNormal);
            fxManager.OnImpact(gameObject, hitPoint, Vector3.Reflect(direction, hitNormal));
            decalManager.CreateDecal(gameObject, hitPoint + hitNormal * 0.05f, -hitNormal);

            return true;
        }
    }
}
