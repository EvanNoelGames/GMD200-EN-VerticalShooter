using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.gameOver += gameIsOver;
    }

    private void gameIsOver()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
