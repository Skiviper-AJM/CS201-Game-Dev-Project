using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 direction;

    void FixedUpdate()
    {
        // Move the projectile in the specified direction
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the projectile if it collides with an enemy or an obstacle
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
