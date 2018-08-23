using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBGScript : ScrollParentScript
{
    private Sprite mySprite;
    private float spriteRealWidth;

    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
        spriteRealWidth = (mySprite.rect.width / mySprite.pixelsPerUnit) * transform.localScale.x;
    }

    private void OnBecameInvisible()
    {
        currentPos = transform.position;
        currentPos.x = currentPos.x + spriteRealWidth * (beginToRight ? -2 : 2);
        transform.position = currentPos;
    }
}
