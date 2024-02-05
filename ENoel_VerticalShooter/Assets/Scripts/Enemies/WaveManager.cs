using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor.PackageManager;

public class WaveManager : MonoBehaviour
{
    public Canvas waveCounterCanvas;
    private TextMeshProUGUI waveText;

    private int waveCount = 0;
    private int bonusCount = 0;
    private int startHealth;
    private int enemiesLeftToAdd;
    private int maxEnemiesOnScreen;

    private Animator anim;

    private int numberOfEnemies;

    private bool earnedUpgrade;
    private bool textShowing;
    private bool waveHappening;
    private bool addingEnemy;

    private bool spawn1Available;
    private bool spawn2Available;
    private bool spawn3Available;

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

    private void Update()
    {
        if (PlayerScore.GetScore() > PlayerScore.GetHighScore())
        {
            PlayerScore.SetHighScore(PlayerScore.GetScore());
        }
        spawn1Available = !spawn1.colliding;
        spawn2Available = !spawn2.colliding;
        spawn3Available = !spawn3.colliding;
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
        addingEnemy = true;
        yield return new WaitForSeconds(0.1f);
        if (spawn2Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyTurret, spawn2.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyRusher, spawn2.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn2.GetPosition(), transform.rotation);
            }
        }
        else if (spawn1Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyTurret, spawn1.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyRusher, spawn1.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn1.GetPosition(), transform.rotation);
            }
        }
        else if (spawn3Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyTurret, spawn3.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                Instantiate(enemyRusher, spawn3.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn3.GetPosition(), transform.rotation);
            }
        }
        addingEnemy = false;
    }

    IEnumerator Co_AddEnemy(int type, int aggression, bool shield, int speed)
    {
        addingEnemy = true;
        yield return new WaitForSeconds(0.1f);
        if (spawn2Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
                Instantiate(a, spawn2.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
                Instantiate(a, spawn2.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn2.GetPosition(), transform.rotation);
            }
        }
        else if (spawn1Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
                Instantiate(a, spawn1.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
                Instantiate(a, spawn1.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn1.GetPosition(), transform.rotation);
            }
        }
        else if (spawn3Available)
        {
            enemiesLeftToAdd--;
            if (type == 1)
            {
                EnemyTurret a = enemyTurret;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speedModifier = speed;
                Instantiate(a, spawn3.GetPosition(), transform.rotation);
            }
            else if (type == 2)
            {
                EnemyRusher a = enemyRusher;
                a.hasShield = shield;
                a.aggression = aggression;
                a.speed = speed;
                Instantiate(a, spawn3.GetPosition(), transform.rotation);
            }
            else if (type == 3)
            {
                EnemySniper a = enemySniper;
                a.aggression = aggression;
                Instantiate(enemySniper, spawn3.GetPosition(), transform.rotation);
            }
        }
        addingEnemy = false;
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
        enemiesLeftToAdd = 12;
        maxEnemiesOnScreen = 8;
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
        startHealth = PlayerHealth.GetHealth();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 10;
        maxEnemiesOnScreen = 6;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        EnemyRusher a = enemyRusher;
        a.aggression = 1;
        a.hasShield = true;
        a.speed = 3;
        Instantiate(enemyRusher, (new Vector3(0, 2, 0)), transform.rotation);
        waveHappening = true;
    }

    IEnumerator Wave3()
    {
        startHealth = PlayerHealth.GetHealth();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 10;
        maxEnemiesOnScreen = 5;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = true;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        waveHappening = true;
    }

    IEnumerator Wave2()
    {
        startHealth = PlayerHealth.GetHealth();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 8;
        maxEnemiesOnScreen = 4;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        waveHappening = true;
    }

    IEnumerator Wave1()
    {
        bonusCount = 0;
        startHealth = PlayerHealth.GetHealth();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 8;
        maxEnemiesOnScreen = 4;
        EnemyTurret a = enemyTurret;
        a.hasShield = false;
        a.speedModifier = 2.0f;
        EnemyTurret b = enemyTurret;
        b.hasShield = false;
        b.speedModifier = 2.0f;
        Instantiate(a, (new Vector3(1, 1, 0)), transform.rotation);
        Instantiate(b, (new Vector3(-1, 1, 0)), transform.rotation);
        waveHappening = true;
    }

    IEnumerator UpdateWaveText()
    {
        waveCount++;
        if (!earnedUpgrade)
        {
            waveText.SetText("Wave " + waveCount.ToString());
        }
        else
        {
            bonusCount++;
            waveText.SetText("Wave " + waveCount.ToString() + "\n" + AddUpgrade(bonusCount));
        }
        waveCounterCanvas.gameObject.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSeconds(3.0f);
        waveCounterCanvas.gameObject.SetActive(false);
        textShowing = true;
        earnedUpgrade = false;
    }

    private void RunWave()
    {
        numberOfEnemies = GetNumberOfEnemies();

        if (waveCount == 1)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                if (startHealth == PlayerHealth.GetHealth())
                {
                    earnedUpgrade = true;
                }
                waveHappening = false;
                StartCoroutine(Wave2());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies && !addingEnemy)
            {
                StartCoroutine(Co_AddEnemy(1, 2, false, 2));
            }
        }
        else if (waveCount == 2)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                if (startHealth == PlayerHealth.GetHealth())
                {
                    earnedUpgrade = true;
                }
                waveHappening = false;
                StartCoroutine(Wave3());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies && !addingEnemy)
            {
                StartCoroutine(Co_AddEnemy(1, 3, false, 4));
            }
        }
        else if (waveCount == 3)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                if (startHealth == PlayerHealth.GetHealth())
                {
                    earnedUpgrade = true;
                }
                waveHappening = false;
                StartCoroutine(Wave4());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies && !addingEnemy)
            {
                if (enemiesLeftToAdd < 2)
                {
                    StartCoroutine(Co_AddEnemy(2, 1, true, 5));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 4, false, 4));
                }
            }
        }
        else if (waveCount == 4)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                if (startHealth == PlayerHealth.GetHealth())
                {
                    earnedUpgrade = true;
                }
                waveHappening = false;
                StartCoroutine(Wave5());
            }
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies && !addingEnemy)
            {
                if (enemiesLeftToAdd < 3)
                {
                    StartCoroutine(Co_AddEnemy(2, 1, true, 7));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 5, false, 4));
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
            else if (enemiesLeftToAdd != 0 && maxEnemiesOnScreen > numberOfEnemies && !addingEnemy)
            {
                if (enemiesLeftToAdd < 3)
                {
                    StartCoroutine(Co_AddEnemy(1, 1, true, 7));
                }
                else
                {
                    StartCoroutine(Co_AddEnemy(1, 5, false, 5));
                }
            }
        }
    }

    private string AddUpgrade(int num)
    {
        if (num == 1)
        {
            PlayerWeaponsManager.laserSpeed += 5f;
            return "Bonus: Laser Speed Up";
        }
        else if (num == 2)
        {
            PlayerWeaponsManager.timeBetweenShots -= 0.25f;
            return "Bonus: Fire Speed Up";
        }
        else if (num == 3)
        {
            PlayerWeaponsManager.laserSpeed += 2f;
            return "Bonus: Laser Speed Up";
        }
        else if (num == 4)
        {
            return "Bonus: Shotgun";
        }
        return "error";
    }
}
