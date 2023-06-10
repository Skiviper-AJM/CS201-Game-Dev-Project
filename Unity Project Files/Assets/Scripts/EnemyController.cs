using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask whatStopsMovement;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public int preferredDistance = 2;
    public LayerMask obstacleMask;

    public BeatController beatController;

    private GameObject player;
    private bool hasMovedOnThisBeat;
    private bool wasBeatOn;

    private EnemySpriteController enemySpriteController; // Reference to the EnemySpriteController
    private Vector3 movePoint; // Use a Vector3 variable for movePoint

    private GameObject activeProjectile; // Reference to the active projectile

    private bool canFireProjectile; // Flag to control firing projectiles

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
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, movePoint) <= .05f)
        {
            if (beatController.isBeatOn && !hasMovedOnThisBeat)
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

                if (!Physics2D.OverlapCircle(transform.position + moveDirection, .2f, whatStopsMovement))
                {
                    movePoint = transform.position + moveDirection; // Update the movePoint
                    hasMovedOnThisBeat = true;
                }

                if ((distanceX <= preferredDistance && sameY) || (distanceY <= preferredDistance && sameX))
                {
                    if (canFireProjectile)
                    {
                        Vector2 directionToPlayerNormalized = (player.transform.position - transform.position).normalized;
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayerNormalized, preferredDistance, obstacleMask);

                        if (hit.collider == null || hit.collider.gameObject == player)
                        {
                            // If the raycast doesn't hit any obstacle or it hits the player, then shoot
                            enemySpriteController.SetFacingDirectionToPlayer(player); // change the sprite direction before shooting
                            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                            newProjectile.GetComponent<EnemyProjectile>().direction = enemySpriteController.GetFacingDirection();
                            activeProjectile = newProjectile;
                            canFireProjectile = false;
                        }
                    }
                }

            }
        }
    }

    void Update()
    {
        // Check if the beat just started
        if (beatController.isBeatOn && !wasBeatOn)
        {
            hasMovedOnThisBeat = false;
        }

        // Update wasBeatOn to reflect the current state
        wasBeatOn = beatController.isBeatOn;

        // Check if the active projectile is destroyed
        if (activeProjectile == null)
        {
            canFireProjectile = true; // Enable firing if there's no active projectile
        }
    }

    public bool HasMovedOnThisBeat()
    {
        return hasMovedOnThisBeat;
    }
}
