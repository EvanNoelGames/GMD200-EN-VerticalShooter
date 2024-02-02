using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject player, buttons, restartButton, homeButton, lastSelectedButton;

    public AudioSource deathSound;

    private void Awake()
    {
        PlayerHealth.gameOver += GameIsOver;
    }

    private void GameIsOver()
    {
        deathSound.Play();
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
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
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
        Destroy(player);
        Time.timeScale = 0f;
        GetComponent<Canvas>().enabled = true;
        buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    // unsubscribe from event on destroy to prevent missing exception error
    private void OnDestroy()
    {
        PlayerHealth.gameOver -= GameIsOver;
    }
}
