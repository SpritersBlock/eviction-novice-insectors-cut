using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SnailMovement : MonoBehaviour
{
    [SerializeField] string[] snailComments; //otherwise known as "snomments"
    [SerializeField] TextMeshPro snailText;

    [SerializeField] Rigidbody rb;
    [SerializeField] float snailSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            snailText.text = snailComments[Random.Range(0, snailComments.Length)];
            //snailText.color = Color.white;
            snailText.DOColor(Color.white, 0.5f).SetEase(Ease.OutQuart);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //snailText.color = Color.clear;
            snailText.DOColor(Color.clear, 0.5f).SetEase(Ease.OutQuart);
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.position + new Vector3(1,0,0) * snailSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
