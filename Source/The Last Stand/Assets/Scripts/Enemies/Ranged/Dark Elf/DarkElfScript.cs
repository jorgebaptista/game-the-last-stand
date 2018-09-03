using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DarkElfScript : EnemyScript
{
    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private float shootForce = 400f;

    [Space]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject arrowPrefab;

    private int arrowPoolID;

    private PoolManagerScript poolManager;

    protected override void Awake()
    {
        base.Awake();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<PoolManagerScript>();
        arrowPoolID = poolManager.PreCache(arrowPrefab, 4);
    }

    private void Shoot()
    {
        GameObject arrow = poolManager.GetCachedPrefab(arrowPoolID);

        arrow.GetComponent<ProjectileScript>().ResetStats(currentDamage);
        arrow.transform.position = shootPoint.position;
        arrow.transform.rotation = shootPoint.rotation;
        arrow.SetActive(true);
        arrow.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * shootForce);

        AudioManagerScript.instance.PlaySound(attackSound, name);
    }

    public void DisableAttack()
    {
        isAttacking = false;
        baseTimer = Time.time;
    }

    #region Debug
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawLine(eyes.position, eyeSight.position);
    }
#endif
    #endregion
}