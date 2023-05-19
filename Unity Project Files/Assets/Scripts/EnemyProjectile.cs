using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector2 movementDirection; // Direction of the projectile
    private float speed; // Speed of the projectile
    private float duration; // Duration of the projectile
    private float timer; // Timer to track the projectile's lifespan

    private void Start()
    {
        timer = 0f;
    }

    public void SetMovement(Vector2 direction, float speed)
    {
        movementDirection = direction.normalized;
        this.speed = speed * 2f; // Double the speed
        duration = 30f; // Set a fixed duration of 30 seconds
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile in the specified direction by two tiles
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Handle collision with the player here
            // For example, you can damage the player or initiate a game over

            Destroy(gameObject);
        }
    }
}
