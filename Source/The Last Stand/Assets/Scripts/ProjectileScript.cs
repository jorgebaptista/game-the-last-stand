using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileScript : MonoBehaviour
{
    [Header("Fade Settings")]
    [Space]
    [SerializeField]
    private float fadeSpeed = 7f;

    protected float currentDamage;

    protected bool isActive;

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidBody2D;
    private Collider2D myCollider2D;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Invoke("Activate", 0.05f);
    }

    private void Activate()
    {
        if (isActiveAndEnabled) isActive = true;
    }

    protected virtual void FixedUpdate()
    {
        if (isActive) transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(myRigidBody2D.velocity.y, myRigidBody2D.velocity.x) * Mathf.Rad2Deg));
    }

    public void ResetStats(float damage)
    {
        currentDamage = damage;

        mySpriteRenderer.color = Color.white;
        myRigidBody2D.isKinematic = false;
        myCollider2D.enabled = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = false;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        isActive = false;

        myCollider2D.enabled = false;
        myRigidBody2D.velocity = Vector2.zero;
        myRigidBody2D.isKinematic = true;

        while (mySpriteRenderer.color.a != 0)
        {
            mySpriteRenderer.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(mySpriteRenderer.color.a, 0f, fadeSpeed * Time.deltaTime));
            yield return null;
        }

        Dismiss();
    }

    protected void Dismiss()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
