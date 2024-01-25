using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] enemyArray;
    [SerializeField] private List<GameObject> activeEnemyList;

    public bool gameRunning = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        activeEnemyList = new List<GameObject>(enemyArray);
    }

    public void UnlistEnemy(GameObject enemy)
    {
        activeEnemyList.Remove(enemy);
        if(activeEnemyList.Count == 0)
        {
            StartCoroutine(Co_ResetAllEnemiesDelayed(2f));
        }
    }

    private void ResetAllEnemies()
    {
        foreach (var enemy in enemyArray)
        {
            enemy.GetComponent<Enemy>().Respawn();
            activeEnemyList.Add(enemy);
        }
    }

    private IEnumerator Co_ResetAllEnemiesDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetAllEnemies();
    }
}
