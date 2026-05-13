using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyTracker : MonoBehaviour
{
    public GameObject enemyIconPrefab;
    public Transform iconContainer;
    public Text tankCountText;

    private List<GameObject> enemies = new List<GameObject>();
    private List<Slider> healthSliders = new List<Slider>();
    private List<Image> iconImages = new List<Image>();

    void Start()
    {
        GameObject[] found = GameObject.FindGameObjectsWithTag("Enemy");
        int tankNumber = 1;
        foreach (GameObject enemy in found)
        {
            enemies.Add(enemy);

            GameObject icon = Instantiate(enemyIconPrefab, iconContainer);
            healthSliders.Add(icon.GetComponentInChildren<Slider>());
            iconImages.Add(icon.GetComponent<Image>());

            // Add label "TANK 1", "TANK 2", etc.
            Text label = icon.GetComponentInChildren<Text>();
            if (label != null)
                label.text = "TANK " + tankNumber;

            Health h = enemy.GetComponent<Health>();
            if (h != null)
                icon.GetComponentInChildren<Slider>().maxValue = h.maxHealth;

            tankNumber++;
        }
        UpdateCount();
    }

    void Update()
    {
        int alive = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                iconImages[i].color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
                healthSliders[i].value = 0;
            }
            else
            {
                alive++;
                Health h = enemies[i].GetComponent<Health>();
                if (h != null)
                    healthSliders[i].value = h.currentHealth;
            }
        }
        tankCountText.text = "TANKS: " + alive;
    }

    void UpdateCount()
    {
        tankCountText.text = "TANKS: " + enemies.Count;
    }
}