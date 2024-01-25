using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        
    }

    private void Update()
    {
        scoreText.SetText(PlayerScore.GetScore().ToString("D9"));
    }
}
