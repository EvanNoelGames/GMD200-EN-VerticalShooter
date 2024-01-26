using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperLaser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    private float _life = 0f;

    void Start()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.TakeDamage();
            Destroy(gameObject);
        }
    }
}
