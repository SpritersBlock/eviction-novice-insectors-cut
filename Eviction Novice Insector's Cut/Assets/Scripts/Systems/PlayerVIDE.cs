﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VIDE_Data;

public class PlayerVIDE : MonoBehaviour
{
    public string playerName = "Garden Boy";
    public DialogueUIManager diagUI;

    //Stored current VA when inside a trigger
    public VIDE_Assign inTrigger;
    [SerializeField] Animator attentionBubbleAnim;

    public static PlayerVIDE instance;

    private void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
        {
            inTrigger = other.GetComponent<VIDE_Assign>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
        {
            inTrigger = null;
        }
    }

    public void attentionBubbleActiveness(bool bubbleActiveness)
    {
        attentionBubbleAnim.SetBool("BubbleActive", bubbleActiveness);
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Submit"))
        {
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        HandleAttentionBubbleActiveness();
    }

    void TryInteract()
    {
        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }
    }

    void HandleAttentionBubbleActiveness()
    {
        if (!VD.isActive)
        {
            if (attentionBubbleAnim.GetBool("BubbleActive") != inTrigger)
            {
                attentionBubbleAnim.SetBool("BubbleActive", inTrigger);
            }
        }
        else
        {
            if (attentionBubbleAnim.GetBool("BubbleActive") == true)
            {
                attentionBubbleAnim.SetBool("BubbleActive", false);
            }
        }
    }
}
