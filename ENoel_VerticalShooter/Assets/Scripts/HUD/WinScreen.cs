using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject buttons, restartButton, homeButton, lastSelectedButton;
    public TextMeshProUGUI scoreText;
    public Canvas canvas;

    public void GameIsOver()
    {
        canvas.enabled = true;
        buttons.SetActive(true);
    }

    private void Update()
    {

        scoreText.SetText(PlayerScore.GetScore().ToString("D9"));

        if (EventSystem.current.currentSelectedGameObject != null && GetComponent<Canvas>().enabled)
        {
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null && GetComponent<Canvas>().enabled)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        canvas.enabled = false;
        buttons.SetActive(false);
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
    }
}