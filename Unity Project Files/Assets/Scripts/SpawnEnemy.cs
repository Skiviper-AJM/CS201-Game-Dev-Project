using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GridGenerator gridGenerator;
    public List<GameObject> enemyPrefabs;
    
    private void OnEnable()
    {
        // Subscribe to the event
        EnemyController.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        EnemyController.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = GetRandomPositionWithinRadius();
        } while (IsPositionOccupied(spawnPosition) || IsPositionNearPlayer(spawnPosition));

        // Instantiate random enemy prefab at the spawn position
        Instantiate(
            enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], 
            spawnPosition, 
            Quaternion.identity
        );
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        float x = Random.Range(-gridGenerator.radius, gridGenerator.radius);
        float y = Random.Range(-gridGenerator.radius, gridGenerator.radius);

        return gridGenerator.centerObject.position + new Vector3(x, y, 0f);
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.4f);

        if (hitCollider != null && hitCollider.gameObject.tag == "Obstacle")
            return true;

        return false;
    }

    private bool IsPositionNearPlayer(Vector3 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 2.1f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
                return true;
        }

        return false;
    }
}
