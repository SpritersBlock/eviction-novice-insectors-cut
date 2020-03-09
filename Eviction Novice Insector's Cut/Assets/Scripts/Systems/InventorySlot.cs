using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemCountText;
    int numberOfThisItem;
    public bool occupied;

    public void AddItemIconToSlot(Item item)
    {
        itemIcon.sprite = item.itemSprite;
        itemIcon.color = Color.white;
        AddOneToThisItemCount();
        itemCountText.gameObject.SetActive(true);
        occupied = true;
    }

    public void AddOneToThisItemCount()
    {
        numberOfThisItem++;
        itemCountText.text = numberOfThisItem.ToString();
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
        occupied = false;
    }
}
