using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    public float spawnTimer = 3f;

    [Space]
    public string waveMusic;

    [Space]
    [SerializeField]
    public EnemyList[] enemyList;

    [System.Serializable]
    public struct EnemyList
    {
        [Header("Enemy Settings")]
        [Space]
        public EnemyType enemyType;

        [Space]
        public int amount;
        public float lifeMultiplier;
        public float damageMultiplier;
        public float attackCooldownMultiplier;
        public float speedMultiplier;

        [Space]
        [Tooltip("Percentage")]
        [Range(0, 100)]
        public float probability;
    }

    private void Awake()
    {
        DebugEnemySettings();
    }

    #region Debug
    private void DebugEnemySettings()
    {
        if (enemyList.Length == 0) Debug.LogError("No enemies have been configured in " + gameObject.name + ".");

        float enemyProbabilities = 0;

        foreach (EnemyList enemy in enemyList)
        {
            enemyProbabilities += enemy.probability;
        }

        //if (enemyProbabilities != 100) Debug.LogError("Total enemy probability of " + gameObject.name + " must equal to 100(%)." +
        //    " Please reconfigure each enemy probability.");

        for (int i = 0; i < enemyList.Length; ++i)
        {
            for (int j = 0; j < enemyList.Length; ++j)
            {
                if (i == j) continue;
                else if (enemyList[i].enemyType == enemyList[j].enemyType)
                    Debug.LogError("There can only be one configured enemy of " + enemyList[i].enemyType + " type in each wave." +
                    "Please reconfigure " + gameObject.name + "'s enemies.");
            }
        }
    }
    #endregion
}