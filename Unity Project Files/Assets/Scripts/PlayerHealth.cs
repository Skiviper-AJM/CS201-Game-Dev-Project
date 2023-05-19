using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if player is dead
        if (currentHealth <= 0)
        {
            // TODO: handle player death
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Prevent overhealing
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
