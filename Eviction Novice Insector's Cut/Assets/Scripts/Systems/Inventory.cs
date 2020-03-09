using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] inventorySlots;

    void AddItemToInventory(Item item)
    {
        //Check if the player already has one (or more) of this item.
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].name == item.name)
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
}
