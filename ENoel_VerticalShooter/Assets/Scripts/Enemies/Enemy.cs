using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float respawnY = 10;
    private float _respawnX;

    private bool cameraCollision = false;
    private bool laserCollision = false;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _respawnX = transform.position.x;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(_respawnX, respawnY);
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Laser"))
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
        cameraCollision = false;
        laserCollision = false;
        PlayerScore.SetScore(PlayerScore.GetScore() + 10);
        gameObject.SetActive(false);
        GameManager.instance.UnlistEnemy(gameObject);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
