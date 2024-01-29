using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperLaser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    private float _life = 0f;

    private bool bouncing = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = -transform.up * 12;
    }


    void Update()
    {
        _life += Time.deltaTime;
        if (_life >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void BounceLaser()
    {
        _life = 0;
        _rb.velocity = -_rb.velocity;
        bouncing = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.TakeDamage();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Rusher"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp)
            {
                Destroy(other.gameObject);
            }
            else
            {
                BounceLaser();
            }
        }
        else if (other.gameObject.CompareTag("Enemy Sniper") && bouncing)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp)
            {
                Destroy(other.gameObject);
            }
            else
            {
                BounceLaser();
            }
        }
        else if (other.gameObject.CompareTag("Enemy Turret") && bouncing)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemy.shieldUp)
            {
                Destroy(other.gameObject);
            }
            else
            {
                BounceLaser();
            }
        }
    }
}
