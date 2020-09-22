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
    public static PlayerMovement instance;

    [Header("Movement")]
    [SerializeField] float currentMovementSpeed = 6;
    [SerializeField] float walkSpeed = 6;
    [SerializeField] float sprintSpeed = 12;
    Vector3 movement;
    [SerializeField] Vector3 lastMove;
    bool sprinting;
    public bool canMove = true;

    [Header("Hop")]
    [SerializeField] float hopSpeed;
    [SerializeField] GameObject hopDustPFX;
    [SerializeField] ParticleSystem sprintPFX;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float groundRaycastLength;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        MovementInput();
    }

    void MovementInput()
    {
        if (!VD.isActive && canMove && !SceneTransition.instance.transitioning && !PauseMenuManager.instance.paused)
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
                StartSprinting();
            }
            if (Input.GetButtonUp("Sprint"))
            {
                StopSprinting();
            }
        }
        else
        {
            movement = Vector3.zero;
            StopSprinting();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleSpriteFlipping();

        if (sprinting)
        {
            SprintPFX();
        }
    }

    void HandleMovement() //For things like speed and animations
    {
        Vector3 newPosition = rb.position + movement.normalized * currentMovementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        anim.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.z) + System.Convert.ToSingle(sprinting));
        anim.SetFloat("Vertical", lastMove.z);
    }

    void HandleSpriteFlipping()
    {
        if (!VD.isActive && !PauseMenuManager.instance.paused)
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

    void StartSprinting()
    {
        sprinting = true;
        currentMovementSpeed = sprintSpeed;
        anim.speed = 2;
        SprintHop();
    }

    void StopSprinting()
    {
        sprinting = false;
        currentMovementSpeed = walkSpeed;
        anim.speed = 1;
    }

    void SprintHop() //If the player is grounded, start sprinting with a short hop
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundRaycastLength, layerMask)) //if player is grounded
        {
            rb.AddForce(new Vector3(0, hopSpeed, 0), ForceMode.Impulse);
            hopDustPFX.SetActive(true);
        }
    }

    void SprintPFX()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundRaycastLength, layerMask)) //if player is grounded
        {
            if (!sprintPFX.isPlaying)
            {
                sprintPFX.Play();
            }
        }
        else
        {
            if (sprintPFX.isPlaying)
            {
                sprintPFX.Stop();
            }
        }
    }
}
