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
            return true;
        }

        return false;
    }
}
