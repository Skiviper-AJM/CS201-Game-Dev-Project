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
                Vector3 moveDirection = Vector3.zero;

                float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
                float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
                bool sameX = Mathf.Approximately(transform.position.x, player.transform.position.x);
                bool sameY = Mathf.Approximately(transform.position.y, player.transform.position.y);

                // if on same X, maintain preferred distance on Y and vice versa
                if (sameX)
                {
                    if(distanceY < preferredDistance) 
                        moveDirection = new Vector3(0, -Mathf.Sign(directionToPlayer.y), 0);
                    else if(distanceY > preferredDistance) 
                        moveDirection = new Vector3(0, Mathf.Sign(directionToPlayer.y), 0);
                }
                else if (sameY)
                {
                    if(distanceX < preferredDistance)
                        moveDirection = new Vector3(-Mathf.Sign(directionToPlayer.x), 0, 0);
                    else if(distanceX > preferredDistance)
                        moveDirection = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0);
                }
                else
                {
                    if (distanceY < distanceX)
                        moveDirection = new Vector3(0, Mathf.Sign(directionToPlayer.y), 0);
                    else if (distanceX <= distanceY)
                        moveDirection = new Vector3(Mathf.Sign(directionToPlayer.x), 0, 0);
                }

                if (!Physics2D.OverlapCircle(movePoint.position + moveDirection, .2f, whatStopsMovement))
                {
                    movePoint.position += moveDirection;
                    hasMovedOnThisBeat = true;
                }

                if ((distanceX <= preferredDistance && sameY) || (distanceY <= preferredDistance && sameX))
                {
                    GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    newProjectile.GetComponent<Projectile>().direction = directionToPlayer;
                    hasMovedOnThisBeat = true;
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
