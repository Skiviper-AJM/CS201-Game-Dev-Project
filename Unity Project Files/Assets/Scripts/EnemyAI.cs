using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public Transform player;
    private BeatManager beatManager;
    private bool isShooting = false; // Alternates between shooting and moving
    private Vector2 movementDirection; // Direction of the enemy's movement

    // Use this for initialization
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
    }

    void Update()
    {
        if (beatManager.IsBeat())
        {
            if (isShooting)
            {
                ShootProjectile();
            }
            else
            {
                MoveTowardsPlayer();
            }

            // Toggle action
            isShooting = !isShooting;
        }
    }

    void ShootProjectile()
    {
        // Instantiate a new projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        // Ignore collision between the projectile and the enemy that shot it
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        // Set the movement direction of the projectile based on the enemy's movement direction
        projectile.GetComponent<EnemyProjectile>().SetMovement(movementDirection, projectileSpeed);
    }

    void MoveTowardsPlayer()
    {
        // Calculate direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        
        // Determine the movement direction based on the absolute value of the direction components
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);
        
        // Check if the movement should be vertical or horizontal
        if (absX > absY)
        {
            // Move horizontally
            transform.position += new Vector3(Mathf.Sign(direction.x), 0f, 0f);
            // Store the movement direction for later use
            movementDirection = new Vector2(Mathf.Sign(direction.x), 0f);
        }
        else
        {
            // Move vertically
            transform.position += new Vector3(0f, Mathf.Sign(direction.y), 0f);
            // Store the movement direction for later use
            movementDirection = new Vector2(0f, Mathf.Sign(direction.y));
        }
    }
}
