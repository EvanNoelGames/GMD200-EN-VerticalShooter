using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    private float _life = 0f;

    private bool bouncing = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = -transform.up * 6;
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
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.TakeDamage();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Rusher"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Sniper") && bouncing)
        {
            Destroy(gameObject);
        }
    }
}
