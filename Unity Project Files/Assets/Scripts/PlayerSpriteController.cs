using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this object. Please add one.");
        }

        playerController = GetComponentInParent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("No PlayerController found in parent objects. Please add one.");
        }

        lastPosition = playerController.transform.position;
    }

    void Update()
    {
        // Check arrow key inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            if (horizontalInput > 0f)
            {
                spriteRenderer.sprite = rightSprite;
            }
            else if (horizontalInput < 0f)
            {
                spriteRenderer.sprite = leftSprite;
            }
            else if (verticalInput > 0f)
            {
                spriteRenderer.sprite = upSprite;
            }
            else if (verticalInput < 0f)
            {
                spriteRenderer.sprite = downSprite;
            }
        }
        else if (playerController.HasMovedOnThisBeat())
        {
            Vector3 moveDirection = playerController.transform.position - lastPosition;

            if (moveDirection.x > 0f)
            {
                spriteRenderer.sprite = rightSprite;
            }
            else if (moveDirection.x < 0f)
            {
                spriteRenderer.sprite = leftSprite;
            }
            else if (moveDirection.y > 0f)
            {
                spriteRenderer.sprite = upSprite;
            }
            else if (moveDirection.y < 0f)
            {
                spriteRenderer.sprite = downSprite;
            }

            lastPosition = playerController.transform.position;
        }
    }

    public Vector3 GetFacingDirection()
    {
        if (spriteRenderer.sprite == rightSprite)
        {
            return Vector3.right;
        }
        else if (spriteRenderer.sprite == leftSprite)
        {
            return Vector3.left;
        }
        else if (spriteRenderer.sprite == upSprite)
        {
            return Vector3.up;
        }
        else if (spriteRenderer.sprite == downSprite)
        {
            return Vector3.down;
        }
        else
        {
            // Default to facing right if no direction is set
            return Vector3.right;
        }
    }
}
