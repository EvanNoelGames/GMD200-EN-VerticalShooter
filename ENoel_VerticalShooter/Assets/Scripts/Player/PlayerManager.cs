using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public GameObject explosionPrefab;

    private Rigidbody2D thisRigidbody;

    public Animator flash;

    private void Awake()
    {
        flash.speed = 0f;
        PlayerHealth.gameOver += GameIsOver;
        PlayerHealth.healthChanged += OnHealthChanged;
    }

    private void GameIsOver()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnHealthChanged(int health)
    {
        flash.speed = 1;
        flash.Play("Flash", 0, 0);
    }

    // unsubscribe from event on destroy to prevent missing exception error
    private void OnDestroy()
    {
        PlayerHealth.gameOver -= GameIsOver;
        PlayerHealth.healthChanged -= OnHealthChanged;
    }
}
