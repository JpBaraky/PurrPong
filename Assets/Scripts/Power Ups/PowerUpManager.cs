using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager: MonoBehaviour {
    public GameObject[] powerUpPrefabs;  // Array of power-up prefabs
    public BoxCollider2D spawnArea;  // Box Collider defining the area where power-ups can spawn
    public float minSpawnTime = 5f;  // Minimum time between power-up spawns
    public float maxSpawnTime = 10f; // Maximum time between power-up spawns

    private void Start() {
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp() {
        yield return new WaitForSeconds(Random.Range(minSpawnTime,maxSpawnTime));
      
        Vector2 randomPosition = GetRandomPositionInsideCollider(spawnArea);
        GameObject randomPowerUp = powerUpPrefabs[Random.Range(0,powerUpPrefabs.Length)];
       
        Instantiate(randomPowerUp,randomPosition,Quaternion.identity);

        StartCoroutine(SpawnPowerUp());
    }

    Vector2 GetRandomPositionInsideCollider(BoxCollider2D collider) {
        
        Vector2 randomPosition = new Vector2(
            Random.Range(collider.bounds.min.x,collider.bounds.max.x),
            Random.Range(collider.bounds.min.y,collider.bounds.max.y)
        );

        randomPosition.x = Mathf.Clamp(randomPosition.x,collider.bounds.min.x,collider.bounds.max.x);
        randomPosition.y = Mathf.Clamp(randomPosition.y,collider.bounds.min.y,collider.bounds.max.y);

        return randomPosition;
    }
}