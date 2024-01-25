using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject buttons, restartButton, lastSelectedButton;

    private void Awake()
    {
        PlayerHealth.gameOver += GameIsOver;
    }

    private void GameIsOver()
    {
        StartCoroutine(Co_WaitForGameOver());
    }

    private void Update()
    {
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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
    }

    IEnumerator Co_WaitForGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
        GetComponent<Canvas>().enabled = true;
        buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }
}
