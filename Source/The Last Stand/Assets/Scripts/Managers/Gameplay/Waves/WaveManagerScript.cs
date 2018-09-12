using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyType
{
    Draugr,
    DarkElf,
    Jottun
}

public class WaveManagerScript : MonoBehaviour
{
    #region Variables
    [Header("Wave Management")]
    [Space]
    [SerializeField]
    private float endWaveTimer = 1.5f;

    [Header("Enemies")]
    [Space]
    [SerializeField]
    [Tooltip("Insert all enemies that will be spawned on the level.")]
    private Enemy[] enemyList;

    [Header("Sounds")]
    [Space]
    [SerializeField]
    private string buildModeMusic = "Build_Music";
    [SerializeField]
    private string waveStartSound = "Wave_Start";
    [SerializeField]
    private string waveEndSound = "Wave_End";

    private List<Enemy> sortedEnemies = new List<Enemy>();

    private float spawnTimer;

    private int currentWave, totalEnemies, enemiesAlive;

    public bool waveActive = false;

    private WaveScript[] waves;
    private GameObject[] spawners;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;
    private PoolManagerScript poolManager;

    [System.Serializable]
    private struct Enemy
    {
        public EnemyType enemyType;
        public GameObject prefab;

        [HideInInspector]
        public int poolID, amount, currentAmount;
        [HideInInspector]
        public float lifeMultiplier, damageMultiplier, attackCooldownMultiplier, speedMultiplier, probability;

        [HideInInspector]
        public bool isActiveThisWave;
    }
    #endregion

    #region Init
    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        levelManager = gameController.GetComponentInChildren<LevelManagerScript>();
        uIManager = gameController.GetComponentInChildren<UIManagerScript>();
        poolManager = gameController.GetComponentInChildren<PoolManagerScript>();

        waves = GetComponentsInChildren<WaveScript>();
        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        DebugScript();

        for (int i = 0; i < enemyList.Length; ++i)
        {
            enemyList[i].poolID = poolManager.PreCache(enemyList[i].prefab);
        }

        SetupWave();
    }

    private void Start()
    {
        AudioManagerScript.instance.PlaySound(buildModeMusic, name);
    }

    private void SetupWave()
    {
        uIManager.UpdateWaveText(currentWave + 1);

        spawnTimer = waves[currentWave].spawnTimer;
        SetUpEnemies();
    }
    #endregion

    #region Enemy Setup
    private void SetUpEnemies()
    {
        totalEnemies = 0;

        for (int i = 0; i < enemyList.Length; ++i)
        {
            enemyList[i].isActiveThisWave = false;
        }

        for (int i = 0; i < waves[currentWave].enemyList.Length; ++i)
        {
            for (int k = 0; k < enemyList.Length; ++k)
            {
                if (enemyList[k].enemyType == waves[currentWave].enemyList[i].enemyType)
                {
                    enemyList[k].amount = waves[currentWave].enemyList[i].amount;
                    enemyList[k].lifeMultiplier = waves[currentWave].enemyList[i].lifeMultiplier;
                    enemyList[k].damageMultiplier = waves[currentWave].enemyList[i].damageMultiplier;
                    enemyList[k].attackCooldownMultiplier = waves[currentWave].enemyList[i].attackCooldownMultiplier;
                    enemyList[k].speedMultiplier = waves[currentWave].enemyList[i].speedMultiplier;
                    enemyList[k].probability = waves[currentWave].enemyList[i].probability;

                    enemyList[k].currentAmount = enemyList[k].amount;
                    enemyList[k].isActiveThisWave = true;

                    totalEnemies += enemyList[k].amount;

                    break;
                }
            }
        }

        SortEnemies();
    }

    private void UpdateEnemies()
    {
        foreach (Enemy enemy in sortedEnemies)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemy.enemyType == enemyList[i].enemyType)
                {
                    enemyList[i] = enemy;
                }
            }
        }

        SortEnemies();
    }

    private void SortEnemies()
    {
        sortedEnemies.Clear();

        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].isActiveThisWave) sortedEnemies.Add(enemyList[i]);
        }

        if (sortedEnemies.Count > 1)
        {
            bool sorted = false;

            while (!sorted)
            {
                for (int i = 0; i < sortedEnemies.Count; i++)
                {
                    for (int j = i + 1; j < sortedEnemies.Count; j++)
                    {
                        if (sortedEnemies[i].probability > sortedEnemies[j].probability)
                        {
                            var temp = sortedEnemies[i];
                            sortedEnemies[i] = sortedEnemies[j];
                            sortedEnemies[j] = temp;

                            sorted = false;
                        }
                        else sorted = true;
                    }
                }
            }
        }
    }
    #endregion

    #region Wave Run Setup
    public void StartWave()
    {
        StopAllCoroutines();
        StartCoroutine(RunWave());

        AudioManagerScript.instance.PlaySound(waveStartSound, name);
        AudioManagerScript.instance.StopSound(waves[currentWave].waveMusic);
        AudioManagerScript.instance.PlaySound(waves[currentWave].waveMusic, name);
    }

    private IEnumerator RunWave()
    {
        int incomingEnemies = totalEnemies;
        enemiesAlive = 0;

        yield return new WaitForSeconds(.5f);
        waveActive = true;

        while (incomingEnemies > 0)
        {
            Enemy pickedEnemy = new Enemy();
            bool enemyPicked = false;

            while (!enemyPicked)
            {
                if (sortedEnemies.Count > 1)
                {
                    float randomValue = Random.Range(0, sortedEnemies[sortedEnemies.Count - 1].probability);

                    for (int i = 0; i < sortedEnemies.Count; ++i)
                    {
                        pickedEnemy = sortedEnemies[i];

                        if (pickedEnemy.probability > randomValue)
                        {
                            if (pickedEnemy.currentAmount > 0 && pickedEnemy.isActiveThisWave)
                            {
                                --pickedEnemy.currentAmount;
                                sortedEnemies[i] = pickedEnemy;

                                enemyPicked = true;
                            }
                            else
                            {
                                pickedEnemy.isActiveThisWave = false;
                                sortedEnemies[i] = pickedEnemy;
                                UpdateEnemies();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    pickedEnemy = sortedEnemies[0];
                    --pickedEnemy.currentAmount;
                    sortedEnemies[0] = pickedEnemy;

                    enemyPicked = true;

                    break;
                }
            }
            --incomingEnemies;
            ++enemiesAlive;

            yield return new WaitForSeconds(spawnTimer);

            SpawnEnemy(pickedEnemy);
        }

        yield return new WaitUntil(() => enemiesAlive == 0);

        waveActive = false;

        if (currentWave < waves.Length - 1) yield return new WaitForSeconds(endWaveTimer);
        else yield return new WaitForSeconds(endWaveTimer + 2f);

        EndWave();
        yield return null;
    }

    public void UpdateEnemiesAlive()
    {
        --enemiesAlive;
    }

    private void SpawnEnemy(Enemy enemyPicked)
    {
        GameObject enemy = poolManager.GetCachedPrefab(enemyPicked.poolID);

        if (enemy != null)
        {
            Transform spawner = spawners[Random.Range(0, spawners.Length)].GetComponent<Transform>();

            enemy.GetComponent<EnemyScript>().UpdateStats(enemyPicked.lifeMultiplier, enemyPicked.damageMultiplier,
                enemyPicked.attackCooldownMultiplier, enemyPicked.speedMultiplier);

            enemy.transform.position = spawner.position;
            enemy.transform.rotation = spawner.rotation;
            enemy.SetActive(true);
        }
        else Debug.LogError("PoolManager not returning Cached Object: " + enemy.name + ".");
    }

    private void EndWave()
    {
        StopAllCoroutines();

        if (currentWave < waves.Length - 1)
        {
            AudioManagerScript.instance.PlaySound(waveEndSound, name);
            AudioManagerScript.instance.StopSound(buildModeMusic);
            AudioManagerScript.instance.PlaySound(buildModeMusic, name);

            ++currentWave;
            SetupWave();
            levelManager.ToggleBuildMode(true);
        }
        else levelManager.GameWin();
    }
    #endregion

    #region Debug
    private void DebugScript()
    {
        for (int i = 0; i < waves.Length; ++i)
        {
            for (int j = 0; j < waves[i].enemyList.Length; ++j)
            {
                bool isEnemyInserted = false;

                for (int k = 0; k < enemyList.Length; ++k)
                {
                    if (enemyList[k].enemyType == waves[i].enemyList[j].enemyType)
                    {
                        isEnemyInserted = true;
                        break;
                    }
                }

                if (!isEnemyInserted) Debug.LogError("Not all Enemy Types have been inserted on <b>" + gameObject.name + "</b>'s list." +
                    " Please check <b>" + waves[i].gameObject.name + "</b> for more information on which Enemy Types.");
            }
        }

        for (int i = 0; i < enemyList.Length; ++i)
        {
            for (int j = 0; j < enemyList.Length; ++j)
            {
                if (i == j) continue;
                else if (enemyList[i].enemyType == enemyList[j].enemyType)
                    Debug.LogError("There can only be one configured enemy of <b>" + enemyList[i].enemyType + " Type</b>." +
                    "Please reconfigure <b>" + gameObject.name + "</b>'s enemy's list.");
            }

            if (enemyList[i].prefab == null) Debug.LogError("GameObject Prefab not set for " + enemyList[i].enemyType + " Enemy Type" +
                " in " + gameObject.name + "'s Enemy List.");
        }

        if (!(waves.Length > 0)) Debug.LogError("There aren't any configured waves. Please create at least one wave by clicking the button 'Create Wave' on "
            + gameObject.name + "'s Game Object.");
    }
    #endregion
}