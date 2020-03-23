using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemOnPickup : MonoBehaviour
{
    [SerializeField] Item item;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Inventory.instance.AddItemToInventory(item);
            Destroy(gameObject);
        }
    }
}
