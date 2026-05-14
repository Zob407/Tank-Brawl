using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject objectivePanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        objectivePanel.SetActive(false);
    }

    public void ShowObjective()
    {
        objectivePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
        objectivePanel.SetActive(false);
    }
}