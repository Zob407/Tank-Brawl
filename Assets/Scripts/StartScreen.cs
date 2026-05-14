using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject controlsPanel;
    public GameObject objectivePanel;
    private float timer = 0f;

    void Start()
{
    Time.timeScale = 0f;
    mainPanel.SetActive(true);
    controlsPanel.SetActive(false);
    objectivePanel.SetActive(false);
}

    void Update()
{
    Time.timeScale = mainPanel.activeSelf ? 0f : 1f;
    timer += Time.unscaledDeltaTime;
}

    public void ShowControls()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
        objectivePanel.SetActive(false);
    }

    public void ShowObjective()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(false);
        objectivePanel.SetActive(true);
    }

    public void Back()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
        objectivePanel.SetActive(false);
    }

    public void StartGame()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(false);
        objectivePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}