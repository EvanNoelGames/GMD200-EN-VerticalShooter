using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    private float _life = 0f;

    private bool bouncing = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * PlayerWeaponsManager.GetLaserSpeed();
    }

    void Update()
    {
        _life += Time.deltaTime;
        if (_life >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy Rusher"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp || PlayerWeaponsManager.GetPenetrationRounds())
            {
                StartCoroutine(Co_DestroyLaserRoutine());
            }
            else
            {
                BounceLaser();
            }
        }
        else if (other.gameObject.CompareTag("Enemy Sniper"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp || PlayerWeaponsManager.GetPenetrationRounds())
            {
                StartCoroutine(Co_DestroyLaserRoutine());
            }
            else
            {
                BounceLaser();
            }
        }
        else if (other.gameObject.CompareTag("Enemy Turret"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp || PlayerWeaponsManager.GetPenetrationRounds())
            {
                StartCoroutine(Co_DestroyLaserRoutine());
            }
            else
            {
                BounceLaser();
            }
        }
        else if (other.gameObject.CompareTag("Player") && bouncing)
        {
            PlayerHealth.TakeDamage();
            if (!PlayerWeaponsManager.GetPenetrationRounds())
            {
                StartCoroutine(Co_DestroyLaserRoutine());
            }
        }
    }

    private void BounceLaser()
    {
        _life = 0;
        _rb.velocity = -_rb.velocity;
        bouncing = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy Laser"))
        {
            Destroy(other.gameObject);
            if (!PlayerWeaponsManager.GetPenetrationRounds())
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Laser") && bouncing)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator Co_DestroyLaserRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
