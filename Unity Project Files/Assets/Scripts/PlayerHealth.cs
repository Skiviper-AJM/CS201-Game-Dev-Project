using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    public Canvas overlayCanvas;
    public Image RedHeartA;
    public Image RedHeartB;
    public Image RedHeartC;
    public Image RedHalfHeartA;
    public Image RedHalfHeartB;
    public Image RedHalfHeartC;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        switch (currentHealth)
        {
            case 5:
                RedHeartC.gameObject.SetActive(false);
                break;
            case 4:
                RedHalfHeartC.gameObject.SetActive(false);
                break;
            case 3:
                RedHeartB.gameObject.SetActive(false);
                break;
            case 2:
                RedHalfHeartB.gameObject.SetActive(false);
                break;
            case 1:
                RedHeartA.gameObject.SetActive(false);
                break;
            case 0:
                RedHalfHeartA.gameObject.SetActive(false);
                Die();
                break;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        switch (currentHealth)
        {
            case 1:
                RedHalfHeartA.gameObject.SetActive(true);
                break;
            case 2:
                RedHeartA.gameObject.SetActive(true);
                break;
            case 3:
                RedHalfHeartB.gameObject.SetActive(true);
                break;
            case 4:
                RedHeartB.gameObject.SetActive(true);
                break;
            case 5:
                RedHalfHeartC.gameObject.SetActive(true);
                break;
            case 6:
                RedHeartC.gameObject.SetActive(true);
                break;
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        overlayCanvas.enabled = true;

        Time.timeScale = 0f;
    }
}
