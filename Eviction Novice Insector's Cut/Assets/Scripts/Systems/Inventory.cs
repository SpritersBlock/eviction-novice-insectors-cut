using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using VIDE_Data;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;
    public string currentlySelectedItem;
    public int amountOfCurrentlySelectedItem;
    [SerializeField] int inventorySelectionIndex;
    public bool inventoryActive;
    [SerializeField] Image selectionBorder;
    public static Inventory instance;

    [Header("Item Blurb Window")]
    [SerializeField] GameObject itemBlurbWindow;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    [Header("Canvas Groups")]
    [SerializeField] CanvasGroup inventorySlotsCanvasGroup;
    [SerializeField] CanvasGroup itemBlurbCanvasGroup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddItemToInventory(Item item)
    {
        //Check if the player already has one (or more) of this item.
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].nameOfHeldItem == item.name)
            {
                inventorySlots[i].AddOneToThisItemCount();
                return;
            }
        }

        //If this is a new item...
        for (int o = 0; o < inventorySlots.Length; o++)
        {
            if (!inventorySlots[o].occupied)
            {
                inventorySlots[o].AddItemIconToSlot(item);
                inventorySlots[o].itemDescription = item.itemDescription;
                PlayerItemCollect.instance.ItemCollect(item);
                return;
            }
        }
    }

    public bool CheckNumberOfThisItem(string itemName)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].nameOfHeldItem == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveAllItemsFromThisSlot(string itemName)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].nameOfHeldItem == itemName)
            {
                for (int o = inventorySlots[i].numberOfThisItem; o > 0; o--)
                {
                    inventorySlots[i].SubtractOneFromThisItemCount();
                }
                inventorySlots[i].itemDescription = null;
                return true;
            }
        }
        return false;
    }

    public void ActivateInventorySelection()
    {
        inventorySlotsCanvasGroup.alpha = 1;
        inventoryActive = true;
        CameraFollow.instance.ChangeDefaultDistance(false, CameraFollow.instance.zoomDefaultDistance);
        selectionBorder.gameObject.SetActive(true);
        currentlySelectedItem = ReturnSelectedItem(Input.GetAxisRaw("Horizontal"));
        UpdateItemBlurb();
        itemBlurbWindow.SetActive(true);
        itemBlurbCanvasGroup.alpha = 0;
        itemBlurbCanvasGroup.DOFade(1, 0.5f);
        PlayerMovement.instance.canMove = false;
    }

    public string ReturnSelectedItem(float direction)
    {
        if (direction < 0 && inventorySelectionIndex > 0)
        {
            inventorySelectionIndex--;
        }
        if (direction > 0 && inventorySelectionIndex < inventorySlots.Length - 1)
        {
            inventorySelectionIndex++;
        }
        selectionBorder.rectTransform.DOLocalMoveX(inventorySlots[inventorySelectionIndex].gameObject.transform.localPosition.x, 0.2f).SetEase(Ease.InOutBack);
        if (inventorySlots[inventorySelectionIndex].nameOfHeldItem != null)
        {
            amountOfCurrentlySelectedItem = inventorySlots[inventorySelectionIndex].numberOfThisItem;
            return inventorySlots[inventorySelectionIndex].nameOfHeldItem;
        }
        else //if name is null
        {
            amountOfCurrentlySelectedItem = 0;
            return null;
        }
    }

    public string SelectItem()
    {
        DeactivateInventorySelection();
        return currentlySelectedItem;
    }

    public void DeactivateInventorySelection() //Deactivates selection menu and sets item to null.
    {
        inventorySlotsCanvasGroup.alpha = 0.75f;
        inventoryActive = false;
        selectionBorder.gameObject.SetActive(false);
        itemBlurbCanvasGroup.DOFade(0, 0.5f).WaitForCompletion();
        itemBlurbWindow.SetActive(false);
        PlayerMovement.instance.canMove = true;
        Invoke("EmptyCurrentSelection", 0.1f);
    }

    void UpdateItemBlurb()
    {
        if (!string.IsNullOrEmpty(currentlySelectedItem))
        {
            itemNameText.text = currentlySelectedItem;
            itemDescriptionText.text = inventorySlots[inventorySelectionIndex].itemDescription;
        }
        else
        {
            itemNameText.text = "Nothing";
            itemDescriptionText.text = "Wow! It's nothing!";
        }
    }

    private void Update()
    {
        if (inventoryActive)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                currentlySelectedItem = ReturnSelectedItem(Input.GetAxisRaw("Horizontal"));
                UpdateItemBlurb();
            }
            if (Input.GetButtonDown("Inventory") || Input.GetButtonDown("Cancel"))
            {
                DeactivateInventorySelection();
                EmptyCurrentSelection();
            }
            if (Input.GetButtonDown("Submit"))
            {
                SelectItem();
            }
        }
        else if (!inventoryActive)
        {
            if (Input.GetButtonDown("Inventory") && !VD.isActive)
            {
                ActivateInventorySelection();
            }
        }
    }

    public void FadeInventorySlots(float endOpacity)
    {
        inventorySlotsCanvasGroup.DOFade(endOpacity, 0.2f).SetEase(Ease.OutQuint);
    }

    void EmptyCurrentSelection()
    {
        currentlySelectedItem = null;
        amountOfCurrentlySelectedItem = 0;
        if (!VD.isActive)
        {
            CameraFollow.instance.ChangeDefaultDistance(true, CameraFollow.instance.playerDefaultDistance);
        }
        else
        {
            CameraFollow.instance.ChangeDefaultDistance(false, CameraFollow.instance.zoomDefaultDistance);
        }
    }
}
