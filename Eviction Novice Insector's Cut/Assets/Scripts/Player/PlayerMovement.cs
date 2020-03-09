using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer playerSprite;

    [Header("Movement")]
    [SerializeField] float currentMovementSpeed = 6;
    [SerializeField] float walkSpeed = 6;
    [SerializeField] float sprintSpeed = 12;
    Vector3 movement;
    [SerializeField] Vector3 lastMove;

    [Header("Hop")]
    [SerializeField] float hopSpeed;
    [SerializeField] GameObject hopDustPFX;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float groundRaycastLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!VD.isActive)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                lastMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, lastMove.z);
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                lastMove = new Vector3(lastMove.x, 0, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetButtonDown("Sprint"))
            {
                currentMovementSpeed = sprintSpeed;
                SprintHop();
            }
            if (Input.GetButtonUp("Sprint"))
            {
                currentMovementSpeed = walkSpeed;
            }
        }
        else
        {
            movement = Vector3.zero;
            currentMovementSpeed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.position + movement.normalized * currentMovementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        anim.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.z));

        anim.SetFloat("Vertical", lastMove.z);

        if (!VD.isActive)
        {
            if (movement.x > 0 && playerSprite.flipX) //Facing right
            {
                playerSprite.flipX = false;
            }
            else if (movement.x < 0 && !playerSprite.flipX) //Facing left
            {
                playerSprite.flipX = true;
            }
        }
    }

    void SprintHop()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundRaycastLength, layerMask)) //if player is grounded
        {
            rb.AddForce(new Vector3(0, hopSpeed, 0), ForceMode.Impulse);
            hopDustPFX.SetActive(true);
        }
    }
}
