using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan = 5f; 
    public Vector3 direction;

    private BeatController beatController;
    private bool hasMovedOnThisBeat = true; // Changed to true so it doesn't move on the first beat

    void Start()
    {
        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }

        // Set the projectile to delete itself after its lifespan expires
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        // Move the projectile 2 tiles per beat
        if (beatController.isBeatOn && !hasMovedOnThisBeat)
        {
            // Normalize direction to ensure it only has a magnitude of 1, and multiply by 2 to move 2 tiles per beat
            direction = direction.normalized * 2;

            transform.position += direction;
            hasMovedOnThisBeat = true;
        }
        else if (!beatController.isBeatOn)
        {
            hasMovedOnThisBeat = false;
        }
    }

    // Detect collision with enemy or obstacle
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
