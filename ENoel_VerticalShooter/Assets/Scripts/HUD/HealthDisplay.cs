using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;

    private void OnEnable()
    {
        PlayerHealth.healthChanged += OnHealthChanged;
        OnHealthChanged(PlayerHealth.GetHealth());
    }

    private void OnDisable()
    {
        PlayerHealth.healthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        // Update UI
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].fillAmount = health - i;
        }
    }
}
