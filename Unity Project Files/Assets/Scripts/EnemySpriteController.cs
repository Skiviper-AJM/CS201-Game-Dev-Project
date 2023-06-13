using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : MonoBehaviour
{
    public Sprite upSprite; 
    public Sprite downSprite; 
    public Sprite leftSprite; 
    public Sprite rightSprite; 

    private SpriteRenderer spriteRenderer; 
    private EnemyController enemyController;

    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this object. Please add one.");
        }

        enemyController = GetComponentInParent<EnemyController>();
        if (enemyController == null)
        {
            Debug.LogError("No EnemyController found in parent objects. Please add one.");
        }

        lastPosition = enemyController.transform.position;
    }

    void Update()
    {
        if(enemyController.HasMovedOnThisBeat())
        {
            Vector3 moveDirection = enemyController.transform.position - lastPosition;

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

            lastPosition = enemyController.transform.position;
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

    public void SetFacingDirectionToPlayer(GameObject player)
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        
        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
        {
            if (directionToPlayer.x > 0)
            {
                spriteRenderer.sprite = rightSprite;
            }
            else
            {
                spriteRenderer.sprite = leftSprite;
            }
        }
        else
        {
            if (directionToPlayer.y > 0)
            {
                spriteRenderer.sprite = upSprite;
            }
            else
            {
                spriteRenderer.sprite = downSprite;
            }
        }
    }

   


}
