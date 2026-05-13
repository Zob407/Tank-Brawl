using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    [Header("Player Lives")]
    public bool isPlayer = false;
    public int maxLives = 3;
    public int currentLives = 3;
    public TextMeshProUGUI livesText;

    [Header("Game Over UI")]
    public GameObject youLoseObject;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("Enemy Health Bar Settings")]
    public bool hideHealthBarUntilClose = false;
    public GameObject healthBarObject;
    public float showDistance = 15f;

    [Header("Score")]
    public int pointsOnDeath = 100;

    [Header("Boss Spawn Tracking")]
    public bool countsTowardBossSpawn = false;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;
    private bool gameOver = false;
    private Transform player;

    void Start()
    {
        if (isPlayer)
        {
            Time.timeScale = 1f;
        }

        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;

        if (isPlayer)
        {
            currentLives = maxLives;
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        GameObject playerObj = GameObject.Find("PlayerTank");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (youLoseObject != null)
        {
            youLoseObject.SetActive(false);
        }

        if (hideHealthBarUntilClose && healthBarObject != null)
        {
            healthBarObject.SetActive(false);
        }

        UpdateHealthBar();
        UpdateLivesText();
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
        if (gameOver) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        Debug.Log(gameObject.name + " healed: " + currentHealth);
    }
     public void ResetHealth()
    {
    currentHealth = maxHealth;
    UpdateHealthBar();
    }
    void Die()
    {
        if (isPlayer)
        {
            LoseLife();
        }
        else
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(pointsOnDeath);
            }

            if (countsTowardBossSpawn && EnemyTracker.instance != null)
            {
                EnemyTracker.instance.EnemyDied();
            }

            if (healthBarObject != null)
            {
                healthBarObject.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    void LoseLife()
    {
        currentLives--;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            YouLose();
            return;
        }

        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        else
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    void YouLose()
    {
        gameOver = true;

        currentLives = 0;
        currentHealth = 0;

        UpdateLivesText();
        UpdateHealthBar();

        if (youLoseObject != null)
        {
            youLoseObject.SetActive(true);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        TankMovement movement = GetComponent<TankMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        TankShoot shoot = GetComponent<TankShoot>();
        if (shoot != null)
        {
            shoot.enabled = false;
        }

        Time.timeScale = 0f;

        Debug.Log("You Lose");
    }
    
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}