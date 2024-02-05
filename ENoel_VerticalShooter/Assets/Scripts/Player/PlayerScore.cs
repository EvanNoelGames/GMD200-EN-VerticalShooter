using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerScore
{
    public static event Action<int> scoreChanged;
    private static int _score = 0;
    private static int _highscore = 0;

    public static int GetScore()
    {
        return _score;
    }

    public static int GetHighScore()
    {
        return _highscore;
    }

    public static void SetHighScore(int highscore)
    {
        _highscore = highscore;
    }

    public static void SetScore(int score)
    {
        if (_score == score)
        {
            return;
        }

        _score = score;
        scoreChanged?.Invoke(_score);
    }
}
