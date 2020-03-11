using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;
    public string currentlySelectedItem;
    [SerializeField] int inventorySelectionIndex;
    public bool inventoryActive;
    [SerializeField] Image selectionBorder;

    [SerializeField] CameraFollow cameraScript;

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
                return;
            }
        }
    }

    public bool CheckNumberOfThisItem(string itemName)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].nameOfHeldItem == itemName && inventorySlots[i].numberOfThisItem >= inventorySlots[i].maxOfThisItem)
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
                return true;
            }
        }
        return false;
    }

    public void ActivateInventorySelection()
    {
        //inventorySelectionIndex = 0;
        inventoryActive = true;
        cameraScript.defaultDistance = cameraScript.zoomDefaultDistance;
        selectionBorder.gameObject.SetActive(true);
        currentlySelectedItem = ReturnSelectedItem(Input.GetAxisRaw("Horizontal"));
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
            return inventorySlots[inventorySelectionIndex].nameOfHeldItem;
        }
        else //if name is null
        {
            return null;
        }
    }

    public string SelectItem()
    {
        DeactivateInventorySelection();
        return currentlySelectedItem;
    }

    public void DeactivateInventorySelection() //Deactivates selection menu and *sets item to null*.
    {
        inventoryActive = false;
        cameraScript.defaultDistance = cameraScript.playerDefaultDistance;
        selectionBorder.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inventoryActive)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                currentlySelectedItem = ReturnSelectedItem(Input.GetAxisRaw("Horizontal"));
            }
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Cancel"))
            {
                DeactivateInventorySelection();
                currentlySelectedItem = null;
            }
            if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Submit"))
            {
                //if (!string.IsNullOrEmpty(currentlySelectedItem))
                //{
                    SelectItem();
                //}
            }
        }
        else if (!inventoryActive)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ActivateInventorySelection();
                //currentlySelectedItem = null;
            }
        }
    }
}
