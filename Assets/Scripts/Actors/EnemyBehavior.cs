using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] float bloodDecalDistance = 2.0f;
    public void OnBulletImpact(Vector3 direction, Vector3 hitPoint, Vector3 hitNormal)
    {
        var fxManager = FXManager.GetEffectsManager();

        fxManager.OnBloodImpact(gameObject, hitPoint, direction);

        // project blood decal if there is something behind
        if (Physics.Raycast(new Ray(hitPoint, direction), out var hitInfo, bloodDecalDistance, ~(1 << LayerMask.NameToLayer("Enemies"))))
        {
            var decalManager = DecalManager.GetDecalManager();
            decalManager.CreateBloodDecal(hitInfo.collider.gameObject, hitInfo.point + hitInfo.normal * 0.05f, -hitInfo.normal);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
