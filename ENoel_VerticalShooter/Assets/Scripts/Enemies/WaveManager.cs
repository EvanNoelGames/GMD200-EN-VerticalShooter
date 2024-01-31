using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class WaveManager : MonoBehaviour
{
    public Canvas waveCounterCanvas;
    private TextMeshProUGUI waveText;

    private int waveCount = 0;
    private int enemiesLeftToAdd;
    private int maxEnemiesOnScreen;

    private Animator anim;

    private int numberOfEnemies;

    private bool textShowing;
    private bool waveHappening;

    public EnemyRusher enemyRusher;
    public EnemyTurret enemyTurret;
    public EnemySniper enemySniper;

    public SpawnPoint spawn1, spawn2, spawn3;

    public WinScreen winScreen;

    // TODO Sometimes multiple enemies are spawned at once, specifically on the later waves, the ones with the if else statements.
    // They always spawn inside eachother
    // Maybe make a spawn enemy coroutine.

    void Start()
    {
        waveText = waveCounterCanvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        anim = waveCounterCanvas.GetComponent<Animator>();
        StartCoroutine(Wave1());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveHappening)
        {
            RunWave();
        }
    }

    // 1 = turret, 2 = rusher, 3 = sniper
    IEnumerator Co_AddEnemy(int type, int aggression, bool shield)
    {
        yield return new WaitUntil(() => AvailableSpawn() != 0);
        if (AvailableSpawn() == 1)
        {
            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn1.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn1.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn1.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
        else if (AvailableSpawn() == 2)
        {

            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn2.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn2.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn2.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
        else if (AvailableSpawn() == 3)
        {
            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn3.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn3.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn3.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
    }

    IEnumerator Co_AddEnemy(int type, int aggression, bool shield, int speed)
    {
        yield return new WaitUntil(() => AvailableSpawn() != 0);
        if (AvailableSpawn() == 1)
        {
            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn1.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn1.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn1.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
        else if (AvailableSpawn() == 2)
        {

            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn2.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn2.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn2.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
        else if (AvailableSpawn() == 3)
        {
            if (type == 1)
            {
                EnemyTurret a = Instantiate(enemyTurret, spawn3.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
            }
            else if (type == 2)
            {
                EnemyRusher a = Instantiate(enemyRusher, spawn3.GetPosition(), transform.rotation);
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
            }
            else if (type == 3)
            {
                EnemySniper a = Instantiate(enemySniper, spawn3.GetPosition(), transform.rotation);
                a.aggression = aggression;
            }
        }
    }

    private int AvailableSpawn()
    {
        if (!spawn2.colliding)
        {
            return 2;
        }
        else if (!spawn1.colliding)
        {
            return 1;
        }
        else if (!spawn3.colliding)
        {
            return 3;
        }
        return 0;
    }

    public int GetNumberOfEnemies()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Enemy Turret");
        GameObject[] sniper = GameObject.FindGameObjectsWithTag("Enemy Sniper");
        GameObject[] rusher = GameObject.FindGameObjectsWithTag("Enemy Rusher");
        int num = 0;

        for (int i = 0;  i < turrets.Length; i++)
        {
            num++;
        }
        for (int j = 0; j < sniper.Length; j++)
        {
            num++;
        }
        for (int k = 0; k < rusher.Length; k++)
        {
            num++;
        }

        return num;

    }

    IEnumerator EndOfGame()
    {
        waveText.SetText("YOU WIN!!");
        waveCounterCanvas.gameObject.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSeconds(3.0f);
        waveCounterCanvas.gameObject.SetActive(false);
        winScreen.GameIsOver();
    }

    IEnumerator Wave5()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 8;
        maxEnemiesOnScreen = 5;
        Instantiate(enemySniper, (new Vector3(0, 6, 0)), transform.rotation);
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = true;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        EnemyRusher a = Instantiate(enemyRusher, (new Vector3(0, 2, 0)), transform.rotation);
        a.aggression = 3;
        a.hasShield = false;
        waveHappening = true;
    }

    IEnumerator Wave4()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 7;
        maxEnemiesOnScreen = 4;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        EnemyRusher a = Instantiate(enemyRusher, (new Vector3(0, 2, 0)), transform.rotation);
        a.aggression = 1;
        a.hasShield = true;
        a.speed = 3;
        waveHappening = true;
    }

    IEnumerator Wave3()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 6;
        maxEnemiesOnScreen = 3;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = true;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        waveHappening = true;
    }

    IEnumerator Wave2()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 5;
        maxEnemiesOnScreen = 2;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        waveHappening = true;
    }

    IEnumerator Wave1()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 4;
        maxEnemiesOnScreen = 2;
        Instantiate(enemyTurret, (new Vector3(-1, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(1, 1, 0)), transform.rotation).hasShield = false;
        waveHappening = true;
    }

    IEnumerator UpdateWaveText()
    {
        waveCount++;
        waveText.SetText("Wave " + waveCount.ToString());
        waveCounterCanvas.gameObject.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSeconds(3.0f);
        waveCounterCanvas.gameObject.SetActive(false);
        textShowing = true;
    }

    private void RunWave()
    {
        numberOfEnemies = GetNumberOfEnemies();

        if (waveCount == 1)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave2());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies)
            {
                enemiesLeftToAdd--;
                StartCoroutine(Co_AddEnemy(1, 1, false));
            }
        }
        else if (waveCount == 2)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave3());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies)
            {
                enemiesLeftToAdd--;
                StartCoroutine(Co_AddEnemy(1, 1, false));
            }
        }
        else if (waveCount == 3)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave4());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies)
            {
                enemiesLeftToAdd--;
                if (enemiesLeftToAdd < 2)
                {
                    StartCoroutine(Co_AddEnemy(2, 1, false, 5));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 1, false));
                }
            }
        }
        else if (waveCount == 4)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave5());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies)
            {
                enemiesLeftToAdd--;
                if (enemiesLeftToAdd < 3)
                {
                    StartCoroutine(Co_AddEnemy(2, 1, true, 7));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 1, false));
                }
            }
        }
        else if (waveCount == 5)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(EndOfGame());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies)
            {
                enemiesLeftToAdd--;
                if (enemiesLeftToAdd < 3)
                {
                    StartCoroutine(Co_AddEnemy(1, 1, true, 7));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 1, false));
                }
            }
        }
    }
}
