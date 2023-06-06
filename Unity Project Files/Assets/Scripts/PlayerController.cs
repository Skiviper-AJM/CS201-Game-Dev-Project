using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Animator anim;
    public bool HasMovedOnThisBeat()
    {
        return hasMovedOnThisBeat;
    }

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

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (beatController.isBeatOn && !hasMovedOnThisBeat)
            {
                if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        hasMovedOnThisBeat = true;
                    }
                }
                else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
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
}