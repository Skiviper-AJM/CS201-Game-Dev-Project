using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Vector2 direction;
    public float moveSpeed = 10f;
    public float lifeTime = 2f; // Maximum life time of the projectile

    private float moveTime = 0.5f; // Time taken to move 1 tile
    private bool isMoving;

    private PlayerHealth playerHealth;
    private EnemyController enemyController;

    void Start()
    {
        StartCoroutine("LifeTimer");
        enemyController = FindObjectOfType<EnemyController>();
        if (enemyController == null)
        {
            Debug.LogError("No EnemyController found in the scene. Please add one.");
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(direction));
        }
    }

    private IEnumerator MoveCoroutine(Vector3 direction)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Obstacle")
        {
            Destroy(gameObject);
            if (other.tag == "Player")
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null && enemyController != null)
                {
                    playerHealth.TakeDamage(enemyController.bulletDamage);
                }
                else
                {
                    if (playerHealth == null)
                    {
                        Debug.LogError("No PlayerHealth component found on the Player GameObject");
                    }

                    if (enemyController == null)
                    {
                        Debug.LogError("EnemyController is not set.");
                    }
                }
            }
        }
    }
}
