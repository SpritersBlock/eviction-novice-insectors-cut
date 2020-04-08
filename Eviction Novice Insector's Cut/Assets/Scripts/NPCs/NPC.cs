using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool canFlip = true;
    [SerializeField] ParticleSystem dustPoof;

    private void OnTriggerStay(Collider other)
    {
        if (canFlip)
        {
            if (other.tag == "Player")
            {
                //NOTE: Bugs default facing left
                if (other.transform.position.x < transform.position.x && spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                else if (other.transform.position.x > transform.position.x && !spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }

    public void MakeBugDisappear()
    {
        Instantiate(dustPoof, transform.position, Quaternion.identity);
        NumberOfBugsLeft.instance.RemoveOneBug();
        Destroy(gameObject);
    }
}
