using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] int totalItemsSpawn = 10;
    [SerializeField] float spawnInterval = 5.0f;
    [SerializeField] float spawnRadius = 20.0f;

    [SerializeField] GameObject itemPrefab;
    [SerializeField] float initialVelocity = 0.0f;

    List<GameObject> objects = new List<GameObject>();

    float nextItemSpawn = 0.0f;

    private Transform playerTransform;
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.GetComponent<Transform>();
        }
    }
    private bool IsPositionSafe(Vector3 pos, out Vector3 point)
    {
        bool ret = NavMesh.SamplePosition(pos, out var hit, 1.0f, NavMesh.AllAreas);
        point = hit.position;
        return ret;
    }
    private void SpawnRandomItem()
    {
        nextItemSpawn = Time.time + spawnInterval;

        int r = UnityEngine.Random.Range(0, 10);

        for (int i = 0; i < 10; i++)
        {
            float angle = (i + r) % 10 * Mathf.PI * 2.0f / 10;
            var vec = new Vector3(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius) + playerTransform.position;
            if (IsPositionSafe(vec, out var pt))
            {
                var o = Instantiate(itemPrefab, pt, Quaternion.identity);
                objects.Add(o);

                if (initialVelocity > 0.0f && o.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddForce((playerTransform.position - o.transform.position).normalized * initialVelocity + Vector3.up * (initialVelocity / 1.2f), ForceMode.VelocityChange);
                }
                return;
            }
        }
    }

    void Update()
    {
        if (playerTransform && !PlayerManager.GetLocalPlayerManager().IsDead)
        {
            if (Time.time >= nextItemSpawn && ((objects.Count < totalItemsSpawn) || totalItemsSpawn == 0))
                SpawnRandomItem();

            for (int i = 0; i < objects.Count;)
            {
                if (!objects[i])
                {
                    objects.RemoveAt(i);
                }
                else i++;
            }
        }
    }
}
