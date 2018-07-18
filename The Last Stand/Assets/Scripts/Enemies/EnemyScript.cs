using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    [Header("Enemy Settings")]
    [Space]
    [SerializeField]
    private float lifePoints = 80f;
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private float movementSpeed = 1f;

    [Space]
    [SerializeField]
    private int moneyPerKill = 50;

    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private Collider2D attackTrigger;

    [Header("Checker Settings")]
    [Space]
    [SerializeField]
    private Transform frontChecker;
    [SerializeField]
    private float checkerRadius = 0.1f;

    [Space]
    [SerializeField]
    private LayerMask playerLayerMask;

    [Header("LifeBar Settings")]
    [Space]
    [SerializeField]
    private Image lifeBarImage;
    [SerializeField]
    private float lifeBarSpeed = 1.5f;

    [Header("Temporary")]
    [Space]
    [SerializeField]
    private float pushBackForce = 200f;

    [Header("Enemy Properties")]

    private bool isAlive;
    private bool isMoving;
    private bool isAttacking;

    private float currentLifePoints;
    private float currentDamage;
    private float currentMovementSpeed;

    private Collider2D[] checkerColliders = new Collider2D[1];

    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;

    [Header("Other References")]
    private GameManagerScript gameManager;
    private WaveManager waveManager;

    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManagerScript>();
        waveManager = FindObjectOfType<WaveManager>();
    }
    private void Start()
    {        
        //Dismiss();
    }
    void OnEnable()
    {
        ResetProperties();
        isMoving = true;
    }
    private void ResetProperties()
    {
        isAlive = true;
        currentLifePoints = lifePoints;
        currentMovementSpeed = movementSpeed;
        lifeBarImage.fillAmount = 1f;
        ToggleAttackTrigger(false);
    }
    public void IncreaseStats(float damageToAdd, float lifeToAdd)
    {
        currentLifePoints = lifeToAdd;
        currentDamage = damageToAdd;
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            if (isMoving)
            {
                myRigidBody2D.velocity = new Vector2(transform.right.x * currentMovementSpeed, myRigidBody2D.velocity.y);
            }

            if (Physics2D.OverlapCircleNonAlloc(frontChecker.position, checkerRadius, checkerColliders,
                playerLayerMask) > 0)
            {
                if (!isAttacking)
                {
                    Attack();
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentLifePoints -= damage;
            if (currentLifePoints <= 0)
            {
                currentLifePoints = 0;                
            }
            UpdateLifeBar();
            if (currentLifePoints == 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        isAlive = false;
        isMoving = false;

        //*****************************
        gameManager.UpdateMoney(moneyPerKill);
        waveManager.UpdateIncomingEnemies();

        Dismiss();
    }
    private void UpdateLifeBar()
    {
        StopCoroutine("UpdateLifeBarImage");
        StartCoroutine("UpdateLifeBarImage");
    }
    private IEnumerator UpdateLifeBarImage()
    {
        float lifePointsFactor = currentLifePoints / lifePoints;

        while (lifeBarImage.fillAmount != lifePointsFactor)
        {
            lifeBarImage.fillAmount = Mathf.MoveTowards(lifeBarImage.fillAmount, lifePointsFactor, lifeBarSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void Attack()
    {
        ToggleAttackTrigger(true);
        isMoving = false;
        myAnimator.SetTrigger("Attack");
        //*****************************Temporary Set for animation later*******************************
        StartCoroutine(PushBackTemporary());
    }
    private IEnumerator PushBackTemporary()
    {
        yield return new WaitForSeconds(0.05f);
        myRigidBody2D.AddForce(new Vector2(-pushBackForce * transform.right.x, pushBackForce));
        yield return new WaitForSeconds(1);
        ToggleAttackTrigger(false);
    }
    private void ToggleAttackTrigger(bool enabled)
    {
        attackTrigger.enabled = enabled;
        isAttacking = enabled == true ? true : false & (isMoving = true);
    }
    private void Dismiss()
    {
        gameObject.SetActive(false);
    }
}
