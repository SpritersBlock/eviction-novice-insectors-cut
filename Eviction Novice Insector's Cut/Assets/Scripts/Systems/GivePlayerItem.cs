using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionallyActive))]
public class GivePlayerItem : MonoBehaviour
{
    [SerializeField] Item item;

    [SerializeField] Vector2 progressNumber;

    public void GiveItemToPlayer()
    {
        GlobalProgressChecker.instance.UpdateConditionallyActiveBool(Mathf.RoundToInt(progressNumber.x), Mathf.RoundToInt(progressNumber.y), false);
        Inventory.instance.AddItemToInventory(item);
    }
}
