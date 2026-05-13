using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject playerTank;

    void Update()
    {
        if (playerTank == null)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}