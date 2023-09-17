using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public PlayerBehavior player;
    public BoomBox boomBox;

    public GameObject[] enemyPrefabs;
    public Transform[] spawnLocations;
    public float spawnCooldown;

    // these two are initialized in MusicBar script, not in the editor
    [HideInInspector]
    public int enemiesRemainingToSpawn;
    [HideInInspector]
    public int enemiesInGame;
    float spawnTimer = 0.0f;

    void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer > spawnCooldown)
        {
            spawnTimer -= spawnCooldown;
            // pick a random spawn location
            Vector2 spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)].position;
            // spawn a random enemy there
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnLocation, Quaternion.identity);
            // set the reference variables of the enemy
            PoliceBehavior policeBehavior = enemy.GetComponent<PoliceBehavior>();
            policeBehavior.player = player;
            policeBehavior.boomBox = boomBox;
            policeBehavior.spawner = this;
            // keep track of enemy count
            enemiesRemainingToSpawn--;
            enemiesInGame++;
        }
    }
}
