using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float moveSpeed = 10f;
    public float lifeTime = 2f; // Maximum life time of the projectile

    private float moveTime = 0.5f; // Time taken to move 1 tile
    private bool isMoving;

    void Start()
    {
        StartCoroutine("LifeTimer");
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(direction));
        }
    }

    private IEnumerator MoveCoroutine(Vector2 direction)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(direction.x, direction.y, 0);

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
        if (other.tag == "Enemy" || other.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
