using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerHealth
{
    public static event Action gameOver;

    public static event Action<int> healthChanged;
    private static int _health = 1;
    private static bool gameRunning = true;

    public static int GetHealth()
    {
        return _health;
    }

    public static void SetHealth(int health)
    {
        if ( _health == health)
        {
            return;
        }

        _health = health;
        healthChanged?.Invoke(_health);
    }

    public static void TakeDamage()
    {
        _health = _health - 1;
        healthChanged?.Invoke(_health);
        if (_health <= 0)
        {
            GameOver();
        }
    }

    public static bool GetGameRunning()
    {
        return gameRunning;
    }

    public static void GameOver()
    {
        gameRunning = false;
        Time.timeScale = 0f;
        gameOver();
    }
}
