//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WaveManager : MonoBehaviour
//{
//    [Header("Debug")]
//    [Space]
//    [SerializeField]
//    private KeyCode pressToSkipTimer = KeyCode.Q;
//    [SerializeField]
//    private KeyCode pressToSkipWave = KeyCode.W;

//    [Header("Wave Manager Settings")]
//    [Space]
//    [SerializeField]
//    private float waveTimer = 15f;
//    [SerializeField]
//    private int maxWaves = 10;

//    [Space]
//    [SerializeField]
//    private float enemyDelay = 3f;
//    [SerializeField]
//    private int maxEnemies = 10;

//    [Space]
//    [SerializeField]
//    private int addMaxEnemies = 5;
//    [SerializeField]
//    private float subEnemyTimer = 0.1f;

//    [Space]
//    //[SerializeField]
//    //private float addEnemyLife = 10f;
//    [SerializeField]
//    private float addEnemyDamage = 5f;

//    [Space]
//    [SerializeField]
//    private GameObject enemyPrefab;

//    [Header("Wave Manager Properties")]
//    private int currentWave = 1;
//    private int currentMaxEnemies;
//    private int enemiesAlive;

//    private float currentWaveTimer;
//    private float currentEnemyDelay;

//    private GameObject avaiableEnemy;
//    private GameObject[] allEnemiesInPool;

//    private GameObject[] enemySpawners;
//    private int spawnerIndex;

//    [Header("Debug")]
//    [SerializeField]
//    private GameObject[] timerObjects;
//    [SerializeField]
//    private GameObject enemy2;

//    private void Awake()
//    {
//        enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");
//    }
//    private void Start()
//    {
//        currentMaxEnemies = maxEnemies;
//        currentEnemyDelay = enemyDelay;

//        StartCoroutine("WaitBeforeWaveStart");
//    }
//    private void Update()
//    {
//        if (Input.GetKeyDown(pressToSkipTimer))
//        {
//            StopCoroutine("WaitBeforeWaveStart");            
//            StartCoroutine("RunWave");
//        }
//        if (Input.GetKeyDown(pressToSkipWave))
//        {
//            StopAllCoroutines();
//            UpdateWave();
//            StartCoroutine("RunWave");
//        }
//        if (Input.GetKeyDown(KeyCode.E))
//        {

//        }
//    }

//    private void UpdateWave()
//    {
//        if (currentWave <= maxWaves)
//        {
//            currentWave++;
//            currentMaxEnemies += addMaxEnemies;
//            currentEnemyDelay -= subEnemyTimer;

//            //allEnemiesInPool = PoolManagerScript.instance.GetAllEnemiesInPool();

//            for (int enemyIndex = 0; enemyIndex < allEnemiesInPool.Length; ++enemyIndex)
//            {
//                //allEnemiesInPool[enemyIndex].GetComponent<EnemyScript>().UpdateStats(addEnemyLife, addEnemyDamage);
//            }
//        }
//        else
//        {
//            //gameManager.Victory();
//        }
//    }
//    private IEnumerator RunWave()
//    {
//        //*********************
//        //GameManagerScript.instance.gameIsOn = true;

//        UpdateWaveTextUI(currentWave);

//        //******************************************************************

//        //GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

//        //for (int i = 0; i < traps.Length; ++i)
//        //{
//        //    traps[i].GetComponent<TrapManagerScript>().Deactivate(false);
//        //}

//        for (int enemiesToSpawn = 0; enemiesToSpawn < currentMaxEnemies; ++enemiesToSpawn)
//        {
//            spawnerIndex = Random.Range(0, enemySpawners.Length);
//            yield return new WaitForSeconds(enemyDelay);
//            if (Random.value > .50)
//            {
//                //avaiableEnemy = poolManager.GetAvaiableEnemy();
//            }
//            else
//            {
//                avaiableEnemy = Instantiate(enemy2);
//            }
//            if (avaiableEnemy != null)
//            {                
//                avaiableEnemy.transform.position = enemySpawners[spawnerIndex].transform.position;
//                avaiableEnemy.transform.rotation = enemySpawners[spawnerIndex].transform.rotation;
//                avaiableEnemy.SetActive(true);
//            }

//            enemiesAlive++;
//        }

//        yield return new WaitWhile(() => enemiesAlive > 0);

//        UpdateWave();
//        StartCoroutine("WaitBeforeWaveStart");
//    }

//    public void SkipBuildMode()
//    {
//        StopCoroutine("WaitBeforeWaveStart");

//        for (int i = 0; i < timerObjects.Length; ++i)
//        {
//            timerObjects[i].SetActive(false);
//        }

//        StartCoroutine("RunWave");
//    }

//    public void UpdateIncomingEnemies()
//    {
//        enemiesAlive--;        
//    }

//    private IEnumerator WaitBeforeWaveStart()
//    {
//        //*********************
//        //GameManagerScript.instance.gameIsOn = false;

//        currentWaveTimer = waveTimer;

//        //******************************************************************
//        //GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

//        //for (int i = 0; i < traps.Length; ++i)
//        //{
//        //    traps[i].GetComponent<TrapManagerScript>().Deactivate(true);
//        //}

//        for (int i = 0; i < timerObjects.Length; ++i)
//        {
//            timerObjects[i].SetActive(true);
//        }

//        while (currentWaveTimer > -1)
//        {
//            //UIManagerScript.instance.UpdateTimer((int)currentWaveTimer);
//            yield return new WaitForSeconds(1f);
//            currentWaveTimer--;
//        }

//        for (int i = 0; i < timerObjects.Length; ++i)
//        {
//            timerObjects[i].SetActive(false);
//        }

//        StartCoroutine("RunWave");
//    }
//    private void UpdateWaveTextUI(int waveText)
//    {
//        //UIManagerScript.instance.UpdateWaveText(waveText);
//    }
//}
