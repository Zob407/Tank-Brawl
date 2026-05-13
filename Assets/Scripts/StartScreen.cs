using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startPanel;
    private float timer = 0f;

    void Start()
    {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer > 1f && startPanel.activeSelf && Input.anyKeyDown)
        {
            startPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}