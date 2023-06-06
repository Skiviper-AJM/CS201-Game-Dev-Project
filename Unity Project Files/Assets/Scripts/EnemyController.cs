using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public int preferredDistance = 2;

    public BeatController beatController;

    private GameObject player;
    private bool hasMovedOnThisBeat;
    private bool wasBeatOn;

    void Start()
    {
        movePoint.parent = null;
        hasMovedOnThisBeat = false;
        wasBeatOn = false;

        player = GameObject.FindGameObjectWithTag("Player");

        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }
    }

    void FixedUpdate()
    {   
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.fixedDeltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (beatController.isBeatOn && !hasMovedOnThisBeat)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer = directionToPlayer.normalized;

                if (!Physics2D.OverlapCircle(movePoint.position + directionToPlayer, .2f, whatStopsMovement))
                {
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= preferredDistance)
                    {
                        if (Mathf.Abs(player.transform.position.y - transform.position.y) <= preferredDistance)
                        {
                            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                            newProjectile.GetComponent<Projectile>().direction = directionToPlayer;
                            hasMovedOnThisBeat = true;
                        }
                    }
                    else
                    {
                        movePoint.position += directionToPlayer;
                        hasMovedOnThisBeat = true;
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
    }

    public bool HasMovedOnThisBeat()
    {
        return hasMovedOnThisBeat;
    }
}
