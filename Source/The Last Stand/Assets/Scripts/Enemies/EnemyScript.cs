using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class EnemyScript : MonoBehaviour
{
#region Variables
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float life = 80f;
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private float attackCooldown = 2f;
    [SerializeField]
    private float moveSpeed = 1f;

    [Space]
    [SerializeField]
    private int money = 50;

    [Header("Eyesight Settings")]
    [Space]
    [SerializeField]
    protected Transform eyes;
    [SerializeField]
    protected Transform eyeSight;
    [SerializeField]
    private LayerMask playerLayer;

    [Header("LifeBar Settings")]
    [Space]
    [SerializeField]
    private Image lifeBarImage;
    [SerializeField]
    private float lifeBarSpeed = 5f;

    protected bool isAlive, isAttacking;

    private float currentLife, currentMoveSpeed;
    protected float currentDamage, currentAttackCooldown;
    protected float baseTimer;

    private Collider2D myCollider2D;
    protected Rigidbody2D myRigidBody2D;
    protected Animator myAnimator;

    private LevelManagerScript LevelManager;
    private WaveManagerScript waveManager;
#endregion

#region Init
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        LevelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<WaveManagerScript>();
    }

    protected virtual void ResetStats()
    {
        isAlive = true;

        currentLife = life;
        currentDamage = damage;
        currentAttackCooldown = attackCooldown;
        currentMoveSpeed = moveSpeed;

        lifeBarImage.fillAmount = 1f;

        myCollider2D.enabled = true;
        myRigidBody2D.isKinematic = false;
    }

    public void UpdateStats(float lifeMultiplier, float damageMultiplier, float attackSpeedMultiplier,float speedMultiplier)
    {
        ResetStats();

        currentLife *= lifeMultiplier;
        currentDamage *= damageMultiplier;
        currentAttackCooldown *= attackSpeedMultiplier;
        currentMoveSpeed *= speedMultiplier;
    }
    #endregion

#region Main
    protected virtual void FixedUpdate()
    {
        if (isAlive && !isAttacking)
        {
            if (Physics2D.Linecast(eyes.position, eyeSight.position, playerLayer))
            {
                if (Time.time > baseTimer + currentAttackCooldown) Attack();
            }
            else myRigidBody2D.velocity = new Vector2(transform.right.x * currentMoveSpeed, myRigidBody2D.velocity.y);

            myAnimator.SetFloat("Horizontal", Mathf.Abs(myRigidBody2D.velocity.x));
        }
    }

    protected abstract void Attack();

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentLife -= damage;

            if (currentLife <= 0) currentLife = 0;

            UpdateLifeBar();

            if (currentLife == 0) Die();
        }
    }

    private void UpdateLifeBar()
    {
        StopCoroutine("UpdateLifeBarImage");
        StartCoroutine("UpdateLifeBarImage");
    }

    private IEnumerator UpdateLifeBarImage()
    {
        float factor = Mathf.Clamp01(currentLife / life);

        while (lifeBarImage.fillAmount != factor)
        {
            lifeBarImage.fillAmount = Mathf.MoveTowards(lifeBarImage.fillAmount, factor, lifeBarSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual void Die()
    {
        isAlive = false;

        myCollider2D.enabled = false;
        myRigidBody2D.isKinematic = true;

        LevelManager.UpdateMoney(money);
        waveManager.UpdateEnemiesAlive();

        //myAnimator.SetTrigger("Die");
        Debug.LogWarning("Unfinished Script.");
        Dismiss();
    }

    private void Dismiss()
    {
        gameObject.SetActive(false);
    }
    #endregion
}