using UnityEngine;

public class HPSPManagement : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxSpirit = 100;
    public int currentHealth;
    public int currentSpirit;
    public HealthBar healthBar;
    public SpiritBar spiritBar;

    void Start()
    {
        currentHealth = maxHealth;
        currentSpirit = maxSpirit;
        healthBar.SetMaxHealth(maxHealth);
        spiritBar.SetMaxSpirit(maxSpirit);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeSP(10);
        }
    }

    void TakeDamage (int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    void TakeSP(int cost)
    {
        currentSpirit -= cost;

        spiritBar.SetSpirit(currentSpirit);

        if (currentSpirit < 0)
        {
            currentSpirit = 0;
        }
        Debug.Log("Current Spirit: " + currentSpirit);
    }
}


