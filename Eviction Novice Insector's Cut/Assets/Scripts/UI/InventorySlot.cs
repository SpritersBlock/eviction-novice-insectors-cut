using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemCountText;
    public int numberOfThisItem;
    public bool occupied;
    public string nameOfHeldItem = null;
    public string itemDescription;
    [SerializeField] Animator anim;

    public void AddItemIconToSlot(Item item)
    {
        itemIcon.sprite = item.itemSprite;
        itemIcon.color = Color.white;
        AddOneToThisItemCount();
        nameOfHeldItem = item.name;
        itemCountText.gameObject.SetActive(true);
        occupied = true;
    }

    public void AddOneToThisItemCount()
    {
        numberOfThisItem++;
        itemCountText.text = numberOfThisItem.ToString();
        itemCountText.transform.DOKill();
        //itemCountText.transform.position =
        itemCountText.transform.DOPunchPosition(Vector3.down * 3, .2f, 10, 1);
        anim.SetTrigger("IconUpdate");
    }

    public void SubtractOneFromThisItemCount()
    {
        numberOfThisItem--;
        itemCountText.text = numberOfThisItem.ToString();
        if (numberOfThisItem <= 0)
        {
            RemoveItemIconFromSlot();
            itemCountText.gameObject.SetActive(false);
        }
    }

    public void RemoveItemIconFromSlot()
    {
        itemIcon.color = Color.clear;
        itemIcon.sprite = null;
        nameOfHeldItem = null;
        occupied = false;
    }
}
