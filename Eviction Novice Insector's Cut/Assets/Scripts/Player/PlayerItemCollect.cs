using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerItemCollect : MonoBehaviour
{
    public static PlayerItemCollect instance;

    [SerializeField] CanvasGroup youGotObject;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    private void Awake()
    {
        instance = this;
    }

    public void ItemCollect(Item item)
    {
        PlayerMovement.instance.canMove = false;
        CameraFollow.instance.currentZoomDistance = CameraFollow.instance.itemZoomDistance;

        PlayerItemNull.instance.itemSprite.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;

        PlayerItemNull.instance.itemSprite.gameObject.SetActive(true);
        PlayerItemNull.instance.itemBackground.gameObject.SetActive(true);

        PlayerItemNull.instance.itemSprite.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        PlayerItemNull.instance.itemSprite.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.7f).SetEase(Ease.OutElastic);

        PlayerItemNull.instance.itemBackground.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        PlayerItemNull.instance.itemBackground.transform.localEulerAngles = new Vector3(0, 0, 180);
        PlayerItemNull.instance.itemBackground.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.7f).SetEase(Ease.OutElastic);
        PlayerItemNull.instance.itemBackground.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).SetEase(Ease.OutElastic);

        youGotObject.alpha = 0;
        youGotObject.gameObject.SetActive(true);
        youGotObject.DOFade(1, 0.4f);

        Inventory.instance.FadeInventorySlots(0.1f);
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
        PlayerItemNull.instance.itemSprite.gameObject.SetActive(false);
        PlayerItemNull.instance.itemBackground.gameObject.SetActive(false);
        youGotObject.gameObject.SetActive(false);
        CameraFollow.instance.currentZoomDistance = CameraFollow.instance.playerDefaultDistance;
        Inventory.instance.FadeInventorySlots(0.75f);
    }
}
