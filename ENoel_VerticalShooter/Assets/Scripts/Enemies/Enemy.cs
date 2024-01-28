using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum type
    {
        rusher,
        sniper
    }

    public type enemyType;
    public bool shieldUp = false;

    private int scoreMultiplier = 1;

    [SerializeField] private GameObject explosionPrefab;

    private bool cameraCollision = false;
    private bool laserCollision = false;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (enemyType == type.sniper)
        {
            scoreMultiplier = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Laser") && !shieldUp)
        {
            laserCollision = true;
        }
        else if (other.gameObject.CompareTag("Camera Zone"))
        {
            cameraCollision = true;
        }

        if (laserCollision && cameraCollision)
        {
            Despawn();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Laser"))
        {
            laserCollision = false;
        }
        else if (other.gameObject.CompareTag("Camera Zone"))
        {
            cameraCollision = false;
        }

    }

    private void Despawn()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        cameraCollision = false;
        laserCollision = false;
        PlayerScore.SetScore(PlayerScore.GetScore() + 10 * scoreMultiplier);
        Destroy(gameObject);
    }
}
