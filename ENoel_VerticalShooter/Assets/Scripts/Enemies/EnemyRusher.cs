using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRusher : MonoBehaviour
{
    public enum state
    {
        moving,
        resting
    }

    public state status;

    private Vector2 restingPos;
    private bool movingTowardsStartingPosition = false;

    private Rigidbody2D _rb;
    public Enemy enemy;

    public GameObject explosionPrefab, shield;

    public bool hasShield = true;
    public bool shieldDestroyed = false;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerPosition;

    public int speed = 10;
    public int aggression;

    private void Awake()
    {
        enemy.hasShield = hasShield;
        enemy.shieldUp = true;
        if (!hasShield && !shieldDestroyed)
        {
            enemy.shieldUp = false;
            Destroy(shield);
            enemy.AddMultiplier(-2);
            shieldDestroyed = true;
        }
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        restingPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Co_Start());
    }

    IEnumerator Co_Start()
    {
        yield return new WaitForSeconds(0.5f);
        Resting();
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
        if (movingTowardsStartingPosition)
        {
            _rb.velocity = (restingPos - _rb.position);
        }
    }

    private void Resting()
    {
        if (hasShield && shield != null)
        {
            enemy.shieldUp = true;
        }
        _rb.gravityScale = 0.002f;

        StartCoroutine(Co_SwitchMode(Random.Range(5f, 15f) / aggression));
    }

    private void Moving()
    {
        _rb.gravityScale = 0.4f;

        // predict the player's movement using playerPosition and playerMovement.GetDirection()
        if (playerMovement != null)
        {
            _rb.velocity = ((playerPosition.position + (playerMovement.GetDirection())) - _rb.position).normalized * speed;
        }
        else
        {
            SwitchMode();
        }
    }

    private void SwitchMode()
    {
        if (status == state.moving)
        {
            status = state.resting;
            Resting();
        }
        else if (status == state.resting)
        {
            if (hasShield && shield != null)
            {
                shield.SetActive(false);
                enemy.shieldUp = false;
            }
            status = state.moving;
            Moving();
        }
    }

    // called in the "EnemyRespawnTrigger" script
    public void Respawn()
    {
        if (hasShield && shield != null)
        {
            shield.SetActive(true);
            enemy.shieldUp = true;
        }
        // reset velocity to 0 to prevent flinging
        _rb.velocity = new Vector2(0f, 0f);

        // x spawn-point variation
        float startingX = Random.Range(6, 8);

        // let the ship come back from either left or right, whichever is closer to the original starting position
        if (restingPos.x < 0)
        {
            startingX = -startingX;
        }

        _rb.MovePosition(new Vector2(startingX, restingPos.y + (Random.Range(-3, 3))));

        StartCoroutine(Co_MoveToRestingPoint());
    }

    IEnumerator Co_SwitchMode(float time)
    {
        yield return new WaitForSeconds(time);
        SwitchMode();
    }

    IEnumerator Co_MoveToRestingPoint()
    {
        movingTowardsStartingPosition = true;
        yield return new WaitUntil(AtRestingPoint);
        _rb.velocity = new Vector2(0, 0);
        SwitchMode();
    }

    // is the ship at the original resting point?
    private bool AtRestingPoint()
    {
        if (TestDifference(_rb.position.x, restingPos.x) && TestDifference(_rb.position.y, restingPos.y))
        {
            movingTowardsStartingPosition = false;
            return true;
        }
        return false;
    }

    // after bouncing off another ship get back to attacking the player
    IEnumerator Co_GetBackOnTrack()
    {
        yield return new WaitForSeconds(0.5f);
        Moving();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && status == state.moving)
        {
            PlayerHealth.TakeDamage();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Rusher") && status == state.moving)
        {
            _rb.velocity = -(_rb.velocity / (Random.Range(2, 4)));
            StartCoroutine(Co_GetBackOnTrack());
        }
    }

    // used to approximate the difference between floats
    private bool TestDifference(float firstPoint, float secondPoint)
    {
        return Mathf.Abs(secondPoint - firstPoint) <= 0.1f;
    }
}
