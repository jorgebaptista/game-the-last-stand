using UnityEngine;

public class ScrollingObjectScript : ScrollParentScript
{
    [SerializeField]
    public bool repeat = true;
    [SerializeField]
    public bool randomizeDirection = false;

    private bool facingRight;
    private Vector2 startPos;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        if (randomizeDirection)
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        startPos = transform.position;
        facingRight = beginToRight;
    }
    
    private void OnBecameInvisible()
    {
        if (repeat)
        {
            currentPos = transform.position;

            if (randomizeDirection)
            {
                RandomizeDirection();
            }

            currentPos = startPos;
            transform.position = currentPos;
            return;
        }

        gameObject.SetActive(false);
    }

    private void RandomizeDirection()
    {
        if (Random.value > .50)
        {
            //change to left
            if (facingRight)
            {
                ChangeCurrentDirection();
            }
        }
        else
        {
            //change to right
            if (!facingRight)
            {
                ChangeCurrentDirection();
            }
        }
    }

    private void ChangeCurrentDirection()
    {
        mySpriteRenderer.flipX = !mySpriteRenderer.flipX;

        startPos.x *= -1;
        scrollSpeed *= -1;
        facingRight = !facingRight;
    }
}