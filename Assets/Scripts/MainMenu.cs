using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject skinPanel;

    [Header("High Score")]
    public TextMeshProUGUI highScoreText;

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level 1");
    }

    public void ShowHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void OpenSkinPanel()
    {
        if (skinPanel != null)
        {
            skinPanel.SetActive(true);
        }
    }

    public void CloseSkinPanel()
    {
        if (skinPanel != null)
        {
            skinPanel.SetActive(false);
        }
    }
}