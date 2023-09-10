using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public PlayerBehavior player;
    public GameObject boomBox;

    public GameObject[] enemyPrefabs;
    public Transform[] spawnLocations;
    public float spawnCooldown;
    float spawnTimer = 0.0f;

    void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer > spawnCooldown)
        {
            spawnTimer -= spawnCooldown;
            Vector2 spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position;
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)], spawnLocation, Quaternion.identity);
            PoliceBehavior policeBehavior = enemy.GetComponent<PoliceBehavior>();
            policeBehavior.player = player;
            policeBehavior.boomBox = boomBox;
        }
    }
}
