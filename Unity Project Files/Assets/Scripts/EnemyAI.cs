using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public Transform player;
    private BeatManager beatManager;
    private bool isShooting = false; // Alternates between shooting and moving

    // Use this for initialization
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
    }

    void Update()
    {
        if (beatManager.IsBeat())
        {
            beatManager.ActedOnBeat();
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
        // Calculate direction towards player
        Vector3 direction = (player.position - transform.position).normalized;
        // Set the projectile's velocity
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    void MoveTowardsPlayer()
    {
        // Calculate direction towards player
        Vector3 direction = (player.position - transform.position).normalized;
        // Move one step towards the player
        transform.position += direction;
    }
}
