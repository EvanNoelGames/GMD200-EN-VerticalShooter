using System.Collections;
using System.Collections.Generic;
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

    private Animator anim;

    private int numberOfEnemies;

    private bool textShowing;
    private bool waveHappening;

    public EnemyRusher enemyRusher;
    public EnemyTurret enemyTurret;
    public EnemySniper enemySniper;

    // TODO use 2D colliders in a few spots on the level as spawn points. when AddEnemy() is called, check if its colliding with anything
    // if it isn't spawn the enemy there.

    // Start is called before the first frame update
    void Start()
    {
        waveText = waveCounterCanvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        anim = waveCounterCanvas.GetComponent<Animator>();
        StartCoroutine(Wave1());
    }

    // Update is called once per frame
    void Update()
    {
        if (waveHappening)
        {
            RunWave();
        }
    }

    // 1 = turret, 2 = rusher, 3 = sniper
    public void AddEnemy(int type)
    {
        return;
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
        Time.timeScale = 1f;
        PlayerScore.SetScore(0);
        PlayerHealth.SetHealth(3);
        PlayerHealth.SetGameRunning(true);
        PlayerWeaponsManager.ResetEverything();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Wave5()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 0;
        Instantiate(enemySniper, (new Vector3(0, 6, 0)), transform.rotation);
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = true;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        EnemyRusher a = Instantiate(enemyRusher, (new Vector3(0, 2, 0)), transform.rotation);
        a.hasShield = false;
        a.aggression = 3;
        waveHappening = true;
    }

    IEnumerator Wave4()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 0;
        Instantiate(enemyTurret, (new Vector3(-2, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = false;
        Instantiate(enemyTurret, (new Vector3(2, 1, 0)), transform.rotation).hasShield = false;
        EnemyRusher a = Instantiate(enemyRusher, (new Vector3(0, 2, 0)), transform.rotation);
        a.SetAggression(10);
        a.SetShield(true);
        waveHappening = true;
    }

    IEnumerator Wave3()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(UpdateWaveText());
        yield return new WaitUntil(() => textShowing == true);
        textShowing = false;
        enemiesLeftToAdd = 0;
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
        enemiesLeftToAdd = 0;
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
        enemiesLeftToAdd = 0;
        Instantiate(enemyTurret, (new Vector3(0, 1, 0)), transform.rotation).hasShield = false;
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
            else if (enemiesLeftToAdd != 0)
            {
                AddEnemy(1);
            }
        }
        else if (waveCount == 2)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave3());
            }
            else if (enemiesLeftToAdd != 0)
            {
                AddEnemy(1);
            }
        }
        else if (waveCount == 3)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave4());
            }
            else if (enemiesLeftToAdd != 0)
            {
                AddEnemy(1);
            }
        }
        else if (waveCount == 4)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(Wave5());
            }
            else if (enemiesLeftToAdd != 0)
            {
                AddEnemy(1);
            }
        }
        else if (waveCount == 5)
        {
            if (enemiesLeftToAdd == 0 && numberOfEnemies == 0)
            {
                waveHappening = false;
                StartCoroutine(EndOfGame());
            }
            else if (enemiesLeftToAdd != 0)
            {
                AddEnemy(1);
            }
        }
    }
}
