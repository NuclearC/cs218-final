using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int totalEnemyCount = 10;
    [SerializeField] float spawnInterval = 5.0f;
    [SerializeField] float spawnRadius = 20.0f;

    [SerializeField] GameObject enemyPrefab;

    private int enemiesSpawned = 0;

    float nextEnemySpawn = 0.0f;

    private Transform playerTransform;
    void Start()
    {
        nextEnemySpawn = spawnInterval;
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.GetComponent<Transform>();
        }
    }
    private bool IsPositionSafe(Vector3 pos)
    {
        return NavMesh.SamplePosition(pos, out var hit, 1.0f, NavMesh.AllAreas);
    }
    private void SpawnRandomEnemy()
    {
        int r = UnityEngine.Random.Range(0, 10);

        for (int i = 0; i < 10; i++)
        {
            float angle = (i + r) % 10 * Mathf.PI * 2.0f / 10;
            var vec = new Vector3(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius) + playerTransform.position;
            if (IsPositionSafe(vec))
            {
                Instantiate(enemyPrefab, vec, Quaternion.identity);
                nextEnemySpawn = Time.time + spawnInterval;
                enemiesSpawned++;
                print("spawned an enemy " + enemiesSpawned);
                return;
            }
        }
    }

    void Update()
    {
        if (playerTransform && !PlayerManager.GetLocalPlayerManager().IsDead)
        {
            if (Time.time >= nextEnemySpawn && enemiesSpawned < totalEnemyCount)
                SpawnRandomEnemy();
        }
    }
}
