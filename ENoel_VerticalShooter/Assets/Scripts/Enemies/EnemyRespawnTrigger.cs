using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Rusher"))
        {
            EnemyRusher enemy = other.gameObject.GetComponent<EnemyRusher>();
            enemy.Respawn();
        }
    }
}
