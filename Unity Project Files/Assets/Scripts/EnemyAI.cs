using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public Transform player;
    private BeatManager beatManager;
    private bool isShooting = false; // Alternates between shooting and moving
    private Vector2 movementDirection; // Direction of the enemy's movement
    private Animator animator; // Reference to the Animator

    // Use this for initialization
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        animator = GetComponent<Animator>();
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

        // Store the movement direction for later use
        movementDirection = GetRestrictedDirection(direction);

        // Move one step towards the player
        transform.position += (Vector3)movementDirection;

        // Update the animator parameters based on the movement direction
        UpdateAnimatorParameters(movementDirection);
    }

        void UpdateAnimatorParameters(Vector2 direction)
    {
        // Reset animator parameters
        animator.SetBool("IsMovingUp", false);
        animator.SetBool("IsMovingDown", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("IsMovingRight", false);

        // Disable the previous animation
        animator.CrossFade("Idle", 0f);

        // Update animator parameters based on the movement direction
        if (direction.y > 0)
            animator.SetBool("IsMovingUp", true);
        else if (direction.y < 0)
            animator.SetBool("IsMovingDown", true);
        else if (direction.x > 0)
            animator.SetBool("IsMovingRight", true);
        else if (direction.x < 0)
            animator.SetBool("IsMovingLeft", true);
    }

    Vector2 GetRestrictedDirection(Vector3 direction)
    {
        // Restrict the direction to up/down and left/right only
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return new Vector2(Mathf.Sign(direction.x), 0f);
        }
        else
        {
            return new Vector2(0f, Mathf.Sign(direction.y));
        }
    }


}
