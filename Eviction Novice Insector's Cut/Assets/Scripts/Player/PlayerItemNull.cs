using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemNull : MonoBehaviour
{
    public static PlayerItemNull instance;
    public SpriteRenderer itemBackground;
    public SpriteRenderer itemSprite;

    private void Awake()
    {
        instance = this;
    }
}
