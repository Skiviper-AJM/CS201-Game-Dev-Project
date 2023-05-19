using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector2 movementDirection; // Direction of the projectile
    private float speed; // Speed of the projectile
    private float duration; // Duration of the projectile
    private float timer; // Timer to track the projectile's lifespan

    public GameObject player; // Reference to the player object

    private void Start()
    {
        timer = 0f;

        // Ignore collision between the projectile and the player
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    public void SetMovement(Vector2 direction, float speed, float duration)
    {
        movementDirection = direction.normalized;
        this.speed = speed;
        this.duration = duration;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile in the specified direction
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Handle collision with the enemy here
            // For example, you can destroy both the projectile and the enemy

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
