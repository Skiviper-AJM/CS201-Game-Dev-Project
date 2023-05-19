using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hit a player, an enemy, or a wall
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
