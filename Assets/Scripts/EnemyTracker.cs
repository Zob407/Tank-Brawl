using UnityEngine;
using TMPro;

public class EnemyTracker : MonoBehaviour
{
    public static EnemyTracker instance;

    [Header("Enemies")]
    public int enemiesAlive = 3;
    public TextMeshProUGUI tanksLeftText;

    [Header("Boss")]
    public GameObject bossObject;
    public Transform bossSpawnPoint;
    public GameObject bossHealthBarObject;

    private bool bossSpawned = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateTanksLeftText();

        if (bossObject != null)
        {
            bossObject.SetActive(false);
        }

        if (bossHealthBarObject != null)
        {
            bossHealthBarObject.SetActive(false);
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive < 0)
        {
            enemiesAlive = 0;
        }

        UpdateTanksLeftText();

        Debug.Log("Enemy died. Enemies left: " + enemiesAlive);

        if (enemiesAlive <= 0 && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;

        if (bossObject == null)
        {
            Debug.LogWarning("Boss Object is missing!");
            return;
        }

        if (bossSpawnPoint != null)
        {
            bossObject.transform.position = bossSpawnPoint.position;
            bossObject.transform.rotation = bossSpawnPoint.rotation;
        }

        bossObject.SetActive(true);

        Health bossHealth = bossObject.GetComponent<Health>();
        if (bossHealth != null)
        {
            bossHealth.ResetHealth();
        }

        EnemyCPU bossAI = bossObject.GetComponent<EnemyCPU>();
        if (bossAI != null)
        {
            bossAI.enabled = true;

            GameObject playerObj = GameObject.Find("PlayerTank");
            if (playerObj != null)
            {
                bossAI.player = playerObj.transform;
            }

            bossAI.wakeUpRange = 999f;
        }

        if (bossHealthBarObject != null)
        {
            bossHealthBarObject.SetActive(true);
        }

        Debug.Log("Boss spawned and AI activated!");
    }

    void UpdateTanksLeftText()
    {
        if (tanksLeftText != null)
        {
            tanksLeftText.text = "Tanks Left: " + enemiesAlive;
        }
    }
}