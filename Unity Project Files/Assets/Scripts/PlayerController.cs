using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public GameObject projectilePrefab; // Reference to the projectile prefab

    public BeatController beatController;

    private bool hasMovedOnThisBeat;
    private bool wasBeatOn;

    void Start()
    {
        movePoint.parent = null;
        hasMovedOnThisBeat = false;
        wasBeatOn = false;

        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (beatController.isBeatOn && !hasMovedOnThisBeat)
            {
                if (Input.GetKey(KeyCode.Space)) // Added code for shooting
                {
                    Vector3 direction = GetComponentInChildren<PlayerSpriteController>().GetFacingDirection();
                    GameObject newProjectile = Instantiate(projectilePrefab, transform.position + direction, Quaternion.identity);
                    newProjectile.GetComponent<Projectile>().direction = direction;
                    hasMovedOnThisBeat = true;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.up, .2f, whatStopsMovement))
                    {
                        movePoint.position += Vector3.up;
                        hasMovedOnThisBeat = true;
                    }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.left, .2f, whatStopsMovement))
                    {
                        movePoint.position += Vector3.left;
                        hasMovedOnThisBeat = true;
                    }
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.down, .2f, whatStopsMovement))
                    {
                        movePoint.position += Vector3.down;
                        hasMovedOnThisBeat = true;
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.right, .2f, whatStopsMovement))
                    {
                        movePoint.position += Vector3.right;
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
