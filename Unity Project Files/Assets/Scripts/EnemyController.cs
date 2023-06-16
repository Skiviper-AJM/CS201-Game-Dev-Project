using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event Action OnEnemyDestroyed;

    public float moveSpeed = 5f;
    public LayerMask whatStopsMovement;
    public GameObject projectilePrefab;
    public int preferredDistance = 2;
    public bool isStationary;

    public int enemyHP = 1;
    public BeatController beatController;
    public int bulletDamage = 1;

    private GameObject player;
    private bool hasMovedOnThisBeat;
    private bool wasBeatOn;

    private Vector3 previousMove;

    private EnemySpriteController enemySpriteController;
    private Vector3 movePoint;

    private GameObject activeProjectile;
    private bool canFireProjectile;

    public int beatBuffer = 0;
    private int beatCount = 0;

    void Start()
    {
        hasMovedOnThisBeat = false;
        wasBeatOn = false;

        player = GameObject.FindGameObjectWithTag("Player");
        enemySpriteController = GetComponentInChildren<EnemySpriteController>();

        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }

        movePoint = transform.position;
        canFireProjectile = true;
    }

    void FixedUpdate()
    {
        if (beatController.isBeatOn && !wasBeatOn)
        {
            beatCount++;
        }

        if (!isStationary)
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
        else
        {
            CheckAndFireProjectile();
        }
    }

    void Update()
    {
        if (beatController.isBeatOn && !wasBeatOn)
        {
            beatCount++;
            hasMovedOnThisBeat = false;
            previousMove = movePoint;
        }

        wasBeatOn = beatController.isBeatOn;

        if (activeProjectile == null)
        {
            canFireProjectile = true;
        }

        if(previousMove != movePoint){
            hasMovedOnThisBeat = true;
        }
    }

    public bool HasMovedOnThisBeat()
    {
        return hasMovedOnThisBeat;
    }

    private void CalculateMoveDirection()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 moveDirection = Vector3.zero;

        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
        bool sameX = Mathf.Approximately(transform.position.x, player.transform.position.x);
        bool sameY = Mathf.Approximately(transform.position.y, player.transform.position.y);

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
            movePoint = transform.position + moveDirection;
            hasMovedOnThisBeat = true;
            beatCount = 0;
        }

        CheckAndFireProjectile();
    }

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
                    enemySpriteController.SetFacingDirectionToPlayer(player);
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
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke();
    }
}
