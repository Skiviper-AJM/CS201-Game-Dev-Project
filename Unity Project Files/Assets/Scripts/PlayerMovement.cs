using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2Int gridPosition; // Player's position on the grid
    private BeatManager beatManager; // Reference to the BeatManager
    private Animator animator; // Reference to the Animator

    // Use this for initialization
    void Start()
    {
        //gets reference for the animator
        animator = GetComponent<Animator>();

        // Assume player starts at the origin
        gridPosition = Vector2Int.zero;

        // Get a reference to the BeatManager
        beatManager = FindObjectOfType<BeatManager>();
    }

    void Update()
    {
        // Get input from player (only on the beat)
        if (beatManager.IsBeat())
        {
            int x = (int)Input.GetAxisRaw("Horizontal");
            int y = (int)Input.GetAxisRaw("Vertical");

            // Reset the animation states
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
            animator.SetBool("IsMovingUp", false);
            animator.SetBool("IsMovingDown", false);

            // Update the player's grid position
            gridPosition += new Vector2Int(x, y);

            //Sets Animation parameters based on direction moved
            if (x > 0) animator.SetBool("IsMovingRight", true);
            else if (x < 0) animator.SetBool("IsMovingLeft", true);
            else if (y > 0) animator.SetBool("IsMovingUp", true);
            else if (y < 0) animator.SetBool("IsMovingDown", true);

            // Move the player
            transform.position = (Vector3Int)gridPosition;

            // Let the BeatManager know that we've acted on the beat
            beatManager.ActedOnBeat();
        }
    }
}
