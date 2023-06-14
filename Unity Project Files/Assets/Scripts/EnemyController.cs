using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask whatStopsMovement;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public int preferredDistance = 2;
    public bool isStationary; // Added boolean to control if the enemy is stationary

    public int enemyHP = 1; //sets the enemies max HP
    public BeatController beatController;
    public int bulletDamage = 1; //amount of damage this enemy types bullets do
    private GameObject player;
    private bool hasMovedOnThisBeat;
    private bool wasBeatOn;

    private Vector3 previousMove;

    private EnemySpriteController enemySpriteController; // Reference to the EnemySpriteController
    private Vector3 movePoint; // Use a Vector3 variable for movePoint

    private GameObject activeProjectile; // Reference to the active projectile

    private bool canFireProjectile; // Flag to control firing projectiles

    public int beatBuffer = 0; // Here's the new integer for buffering beats
    private int beatCount = 0; // Here's a counter for beats

    void Start()
    {
        hasMovedOnThisBeat = false;
        wasBeatOn = false;

        player = GameObject.FindGameObjectWithTag("Player");
        enemySpriteController = GetComponentInChildren<EnemySpriteController>(); // Get the EnemySpriteController from child objects

        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }

        movePoint = transform.position; // Set movePoint to the initial position

        canFireProjectile = true; // Enable firing projectile on start
    }

    void FixedUpdate()
    {
        if (beatController.isBeatOn && !wasBeatOn)
        {
            beatCount++;
        }

        if (!isStationary) // Enemy will only move if isStationary is false
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, movePoint) <= .05f)
            {
                if (beatController.isBeatOn && !hasMovedOnThisBeat && beatCount > beatBuffer)
                {
                    CalculateMoveDirection();
                    beatCount = 0;
                }
            }
        }
        else // Even if the enemy is stationary, we still want it to fire projectiles
        {
            CheckAndFireProjectile();
        }
    }

    void Update()
    {
        // Check if the beat just started
        if (beatController.isBeatOn && !wasBeatOn)
        {
            beatCount++;
            hasMovedOnThisBeat = false;
            previousMove = movePoint;
        }

        // Update wasBeatOn to reflect the current state
        wasBeatOn = beatController.isBeatOn;

        // Check if the active projectile is destroyed
        if (activeProjectile == null)
        {
            canFireProjectile = true; // Enable firing if there's no active projectile
        }

        if(previousMove != movePoint){
            hasMovedOnThisBeat = true;
        }
    }

    public bool HasMovedOnThisBeat()
    {
        return hasMovedOnThisBeat;
    }

    // Move the calculation of move direction to its own function
    private void CalculateMoveDirection()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 moveDirection = Vector3.zero;

        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
        bool sameX = Mathf.Approximately(transform.position.x, player.transform.position.x);
        bool sameY = Mathf.Approximately(transform.position.y, player.transform.position.y);

        // if on same X, maintain preferred distance on Y and vice versa
        if (sameX)
        {
            if (distanceY < preferredDistance)
                moveDirection = new Vector3(0, -Mathf.Sign(directionToPlayer.y), 0);
            else if (distanceY > preferredDistance)
                moveDirection = new Vector3(0, Mathf.Sign(directionToPlayer.y), 0);
        }
        else if (sameY)
        {
            if (distanceX < preferredDistance)
                moveDirection = new Vector3(-Mathf.Sign(directionToPlayer.x), 0, 0);
            else if (distanceX > preferredDistance)
                moveDirection = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0);
        }
        else
        {
            if (distanceY < distanceX)
                moveDirection = new Vector3(0, Mathf.Sign(directionToPlayer.y), 0);
            else if (distanceX <= distanceY)
                moveDirection = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0);
        }

        if (!Physics2D.OverlapCircle(transform.position + moveDirection, .2f, whatStopsMovement) && beatCount > beatBuffer)
        {
            movePoint = transform.position + moveDirection; // Update the movePoint
            hasMovedOnThisBeat = true;
            beatCount = 0;
        }

        CheckAndFireProjectile();
    }

    // Move the check and fire projectile logic to its own function
    private void CheckAndFireProjectile()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
        bool sameX = Mathf.Approximately(transform.position.x, player.transform.position.x);
        bool sameY = Mathf.Approximately(transform.position.y, player.transform.position.y);

        if (((distanceX <= preferredDistance && sameY) || (distanceY <= preferredDistance && sameX)) || ((sameX && isStationary && distanceX >= preferredDistance) || (sameY && isStationary && distanceY >= preferredDistance)))
        {
            if (canFireProjectile && previousMove == movePoint)
            {
                Vector2 directionToPlayerNormalized = (player.transform.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayerNormalized, preferredDistance, whatStopsMovement);

                if (hit.collider == null || hit.collider.gameObject == player)
                {
                    // If the raycast doesn't hit any obstacle or it hits the player, then shoot
                    enemySpriteController.SetFacingDirectionToPlayer(player); // change the sprite direction before shooting
                    GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    newProjectile.GetComponent<EnemyProjectile>().direction = enemySpriteController.GetFacingDirection();
                    activeProjectile = newProjectile;
                    canFireProjectile = false;
                    beatCount = 0;
                }
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // reduce player health
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }

            // destroy the enemy
            Destroy(gameObject);
        }
    }
}
