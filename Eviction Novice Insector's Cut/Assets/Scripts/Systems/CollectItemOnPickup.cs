using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionallyActive))]
public class CollectItemOnPickup : MonoBehaviour
{
    [SerializeField] Item item;

    [SerializeField] Vector2 progressNumber;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalProgressChecker.instance.UpdateConditionallyActiveBool(Mathf.RoundToInt(progressNumber.x), Mathf.RoundToInt(progressNumber.y), false);
            Inventory.instance.AddItemToInventory(item);
            Destroy(gameObject);
        }
    }
}
