using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PalisadeScript : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float life = 100f;

    [Space]
    [SerializeField]
    [Range(0, 1)]
    private float damagedLifePercent = 0.3f;
    [SerializeField]
    private Sprite damagedSprite;

    [Header("Life Bar Settings")]
    [Space]
    [SerializeField]
    private float lifeBarSpeed = 5;
    [SerializeField]
    private Image lifeBarImage;
    [SerializeField]
    private GameObject lifeBarCanvas;

    private float currentLife;

    private bool isAlive = true;

    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        currentLife = life;
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentLife -= damage;

            if (currentLife < life * damagedLifePercent) mySpriteRenderer.sprite = damagedSprite;
            if (currentLife <= 0) currentLife = 0;

            UpdateLifeBar();

            if (currentLife == 0) Dismiss();
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

    private void Dismiss()
    {
        isAlive = false;
        GetComponentInParent<TrapSpotScript>().isEmpty = true;
        Destroy(gameObject);
    }
}