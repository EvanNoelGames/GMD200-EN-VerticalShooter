using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject lastSelectedButton;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
    }

    private void Update()
    {
        // do not let the mouse deselect a menu icon
        if (EventSystem.current.currentSelectedGameObject != null && GetComponent<Canvas>().enabled)
        {
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null && GetComponent<Canvas>().enabled)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }
    }
}
