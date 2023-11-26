using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwapper : MonoBehaviour
{

    public Sprite[] sprites;
    public float timeStep;

    SpriteRenderer spriteRenderer;
    int curSprite;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(ChangeSprite), 0, timeStep);
    }

    public void ChangeSprite()
    {

        if(curSprite == sprites.Length)
        {
            curSprite = 0;
        }

        spriteRenderer.sprite = sprites[curSprite];
        curSprite++;
    }



}
