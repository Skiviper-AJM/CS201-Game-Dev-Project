using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public Transform movePoint; // Reference to the player's current position

    public LayerMask whatStopsMovement; //lets me set which layer of items stops movements, in this case layer 8

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; // Detaches the movePoint from its parent
    }

    // Update is called once per frame
    void Update()
    {   
        // Controls the movement of the player
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Checks if the player has reached the movePoint within a certain distance
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            // Moves the player on the horizontal axis if the input is either 1 or -1 
            if(Mathf.Abs (Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            } else // Moves the player on the vertical axis if the input is either 1 or -1 (else statement prevents diagonal movement)
            if(Mathf.Abs (Input.GetAxisRaw("Vertical")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }

                     
        } 
    }
}
