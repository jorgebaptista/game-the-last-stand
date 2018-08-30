using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    private float lifeBarSpeed = 5f;
    [SerializeField]
    private Image lifeBarImage;
    [SerializeField]
    private GameObject lifeBarCanvas;

    [Header("Death Settings")]
    [Space]
    [SerializeField]
    private float fadeSpeed = 2f;
    [SerializeField]
    private float burySpeed = 80f;

    protected bool isAlive, isAttacking;

    private float currentLife, currentMoveSpeed;
    protected float currentDamage, currentAttackCooldown;
    protected float baseTimer;

    private Collider2D myCollider2D;
    protected Rigidbody2D myRigidBody2D;
    private SpriteRenderer mySpriteRenderer;
    protected Animator myAnimator;

    private LevelManagerScript LevelManager;
    private WaveManagerScript waveManager;
#endregion

#region Init
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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

        lifeBarCanvas.SetActive(true);
        lifeBarImage.fillAmount = 1f;

        myCollider2D.enabled = true;
        myRigidBody2D.isKinematic = false;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
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

    #region Life
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

        if (lifeBarImage.fillAmount == 0) lifeBarCanvas.SetActive(false);
    }

    protected virtual void Die()
    {
        isAlive = false;
        myRigidBody2D.velocity = new Vector2(0, myRigidBody2D.velocity.y);

        myCollider2D.enabled = false;
        myRigidBody2D.isKinematic = true;

        LevelManager.UpdateMoney(money);
        waveManager.UpdateEnemiesAlive();

        myAnimator.SetTrigger("Die");
        myAnimator.SetBool("Is Alive", false);
    }

    public void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        myRigidBody2D.velocity = new Vector2(0, -burySpeed * Time.deltaTime);

        while (mySpriteRenderer.color.a != 0)
        {
            mySpriteRenderer.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(mySpriteRenderer.color.a, 0f, fadeSpeed * Time.deltaTime));
            yield return null;
        }

        Dismiss();
    }

    private void Dismiss()
    {
        myAnimator.SetBool("Is Alive", true);
        gameObject.SetActive(false);
    }
    #endregion
}