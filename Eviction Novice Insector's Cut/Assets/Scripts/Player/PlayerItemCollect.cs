using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerItemCollect : MonoBehaviour
{
    public static PlayerItemCollect instance;
    [SerializeField] SpriteRenderer itemBackground;
    [SerializeField] SpriteRenderer itemSprite;

    [SerializeField] GameObject youGotObject;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    private void Awake()
    {
        instance = this;
    }

    public void ItemCollect(Item item)
    {
        PlayerMovement.instance.canMove = false;
        itemSprite.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;
        itemSprite.gameObject.SetActive(true);
        itemBackground.gameObject.SetActive(true);
        youGotObject.SetActive(true);
        CameraFollow.instance.currentZoomDistance = CameraFollow.instance.itemZoomDistance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!PlayerMovement.instance.canMove)
            {
                ResumeGameplay();
            }
        }
    }

    void ResumeGameplay()
    {
        PlayerMovement.instance.canMove = true;
        itemSprite.gameObject.SetActive(false);
        itemBackground.gameObject.SetActive(false);
        youGotObject.SetActive(false);
        CameraFollow.instance.currentZoomDistance = CameraFollow.instance.playerDefaultDistance;
    }
}
