using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public GameObject explosionPrefab;

    private Rigidbody2D thisRigidbody;

    private void Awake()
    {
        PlayerHealth.gameOver += GameIsOver;
    }

    private void GameIsOver()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    // unsubscribe from event on destroy to prevent missing exception error
    private void OnDestroy()
    {
        PlayerHealth.gameOver -= GameIsOver;
    }
}
