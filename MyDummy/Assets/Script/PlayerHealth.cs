using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10000;
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private TextMeshProUGUI healthText;

    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = Mathf.Clamp(startingHealth, 0, maxHealth);
        UpdateHealthText();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "Hp" + currentHealth + "/" + maxHealth;
    }
}
