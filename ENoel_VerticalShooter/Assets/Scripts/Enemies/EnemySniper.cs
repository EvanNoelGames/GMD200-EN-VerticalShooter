using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class EnemySniper : MonoBehaviour
{
    public enum state
    {
        moving,
        shooting
    }

    public state status;

    public float speedModifier = 2.0f;
    private Vector2 moveVector;
    private float boundsLeft = -3f;
    private float boundsRight = 3f;
    private int direction = 1, oldDirection;

    private Rigidbody2D _rb;

    public GameObject bulletPrefab, shield;
    public Transform bulletSpawnLocation;

    private bool shieldUp = true;

    private void Awake()
    {
        status = state.moving;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Co_SwitchMode(Random.Range(3f, 10f)));
    }

    private void Update()
    {
        moveVector = new Vector2(direction * speedModifier, 0);
    }

    private void FixedUpdate()
    {
        if (status == state.moving)
        {
            Move();
        }
        else if (status == state.shooting)
        {
            _rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void Move()
    {
        _rb.velocity = moveVector;
        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, boundsLeft, boundsRight);
        transform.position = position;

        // bounce off screen edges
        if (position.x <= boundsLeft)
        {
            direction = 1;
        }
        else if (position.x >= boundsRight)
        {
            direction = -1;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        StartCoroutine(Co_SwitchMode(1.0f));
    }

    private void SwitchMode()
    {
        if (status == state.moving)
        {
            shieldUp = false;
            shield.SetActive(false);
            status = state.shooting;
            oldDirection = direction;
            direction = 0;
            StartCoroutine(Co_ShootTimer(Random.Range(1f, 3f)));
        }
        else
        {
            shieldUp = true;
            shield.SetActive(true);
            status = state.moving;
            direction = oldDirection;
            StartCoroutine(Co_SwitchMode(Random.Range(3f, 10f)));
        }
    }

    // switch mode (moving or shooting) after specified time
    IEnumerator Co_SwitchMode(float time)
    {
        yield return new WaitForSeconds(time);
        SwitchMode();
    }

    // shoot bullet after specified time
    IEnumerator Co_ShootTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Shoot();
    }


    // Enemy Script
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float respawnY = 10;
    private float _respawnX;

    private bool cameraCollision = false;
    private bool laserCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        _respawnX = transform.position.x;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(_respawnX, respawnY);
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
        cameraCollision = false;
        laserCollision = false;
        PlayerScore.SetScore(PlayerScore.GetScore() + 30);
        gameObject.SetActive(false);
        GameManager.instance.UnlistEnemy(gameObject);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
