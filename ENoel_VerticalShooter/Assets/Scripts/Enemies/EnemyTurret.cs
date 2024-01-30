using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class EnemyTurret : MonoBehaviour
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
    private float boundsUp = 5f;
    private float boundsDown = -2f;
    private Vector2 direction;

    private Rigidbody2D _rb;

    public Enemy enemy;

    public bool hasShield = false;
    public bool shieldDestroyed = false;
    private bool directionFlippedY = false, directionFlippedX = false;

    public GameObject bulletPrefab, shield;
    public Transform bulletSpawnLocation;

    public int aggression = 1;

    private void Awake()
    {
        direction = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f)).normalized;
        status = state.moving;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Co_Start());
    }

    IEnumerator Co_Start()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Co_SwitchMode(Random.Range(3f, 6f) / aggression));
    }

    public void Update()
    {
        if (!hasShield && !shieldDestroyed)
        {
            enemy.shieldUp = false;
            Destroy(shield);
            enemy.AddMultiplier(-2);
            shieldDestroyed = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        // NO MATTER WHAT do not leave the screen (safety)
        position.x = Mathf.Clamp(transform.position.x, boundsLeft -0.5f, boundsRight +0.5f);
        position.y = Mathf.Clamp(transform.position.y, boundsDown -0.5f, boundsUp +0.5f);

        moveVector = direction * speedModifier;

        // if it does touch bounds make sure to bounce off 
        if (transform.position.x < boundsLeft || transform.position.x > boundsRight)
        {
            if (!directionFlippedX)
            {
                StartCoroutine(Co_FlipDirectionX());
            }
        }
        else if (transform.position.y < boundsDown || transform.position.y > boundsUp)
        {
            if (!directionFlippedY)
            {
                StartCoroutine(Co_FlipDirectionY());
            }
        }

        if (status == state.moving)
        {
            Move();
        }
        else if (status == state.shooting)
        {
            _rb.velocity = _rb.velocity / 2;
        }

        // set position to the clamped version
        transform.position = position;
    }

    private void Move()
    {
        _rb.velocity = moveVector;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        StartCoroutine(Co_SwitchMode(0.1f));
    }

    private void SwitchMode()
    {
        if (status == state.moving)
        {
            if (hasShield)
            {
                enemy.shieldUp = false;
                shield.SetActive(false);
            }
            status = state.shooting;
            direction = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f)).normalized;
            StartCoroutine(Co_ShootTimer(Random.Range(1f, 3f)));
        }
        else if (status == state.shooting)
        {
            directionFlippedY = false;
            directionFlippedX = false;
            if (hasShield)
            {
                enemy.shieldUp = true;
                shield.SetActive(true);
            }
            status = state.moving;

            StartCoroutine(Co_SwitchMode(Random.Range(3f, 6f) / aggression));
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

    // flip direction on X
    IEnumerator Co_FlipDirectionX()
    {
        directionFlippedX = true;
        direction.x = -direction.x;
        yield return new WaitForSeconds(1.05f);
        directionFlippedX = false;
    }

    // flip direction on Y
    IEnumerator Co_FlipDirectionY()
    {
        directionFlippedY = true;
        direction.y = -direction.y;
        yield return new WaitForSeconds(1.05f);
        directionFlippedY = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Rusher") && status == state.moving)
        {
            if (status == state.shooting)
            {
                SwitchMode();
            }
            direction = -direction;
        }
        else if (other.gameObject.CompareTag("Enemy Turret") && status == state.moving)
        {
            if (status == state.shooting)
            {
                SwitchMode();
            }
            direction = -direction;
        }
    }

    public void SetAggression(int val)
    {
        aggression = val;
    }

    public void SetShield(bool val)
    {
        hasShield = val;
    }

    // used to approximate the difference between floats
    private bool TestDifference(float firstPoint, float secondPoint)
    {
        return Mathf.Abs(secondPoint - firstPoint) <= 0.1f;
    }
}
