using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{

    public static event Action gameOver;

    [ContextMenu("damage")]
    public void TakeDamage()
    {
        PlayerHealth.SetHealth(PlayerHealth.GetHealth() - 1);

        if (PlayerHealth.GetHealth() <= 0)
        {
            GameOver();
        } 
    }

    public void GameOver()
    {
        PlayerHealth.SetGameOver();
        Time.timeScale = 0f;
        gameOver();
    }
}
