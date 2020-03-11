using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemOnPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventory.AddItemToInventory(item);
            Destroy(gameObject);
        }
    }
}
