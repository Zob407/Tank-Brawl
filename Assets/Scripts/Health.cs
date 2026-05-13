using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    [Header("Enemy Health Bar Settings")]
    public bool hideHealthBarUntilClose = false;
    public GameObject healthBarObject;
    public float showDistance = 15f;

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.Find("PlayerTank");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (hideHealthBarUntilClose && healthBarObject != null)
        {
            healthBarObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!hideHealthBarUntilClose) return;
        if (player == null || healthBarObject == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= showDistance && currentHealth > 0)
        {
            healthBarObject.SetActive(true);
        }
        else
        {
            healthBarObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log(gameObject.name + " health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " destroyed!");

        if (healthBarObject != null)
        {
            healthBarObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}