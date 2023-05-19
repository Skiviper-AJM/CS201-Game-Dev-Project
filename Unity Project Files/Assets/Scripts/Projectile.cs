using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 movementDirection;
    private float speed;
    private float duration;
    private float timer;

    public void SetMovement(Vector2 direction, float speed, float duration)
    {
        movementDirection = direction.normalized;
        this.speed = speed;
        this.duration = duration;
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
