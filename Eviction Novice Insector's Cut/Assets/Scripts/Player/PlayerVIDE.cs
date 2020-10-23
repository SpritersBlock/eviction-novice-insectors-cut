using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VIDE_Data;

public class PlayerVIDE : MonoBehaviour
{
    public string playerName = "Garden Boy";

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

    private void OnTriggerStay(Collider other)
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
        if (Input.GetButtonDown("Submit"))
        {
            TryInteract();
        }
        if (VD.isActive)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                DialogueUIManager.instance.EndDialogue(VD.nodeData);
            }
        }
    }

    private void FixedUpdate()
    {
        HandleAttentionBubbleActiveness();
    }

    void TryInteract()
    {
        if (inTrigger && !SceneTransition.instance.transitioning && !PauseMenuManager.instance.paused && !PlayerItemCollect.instance.inItemGetScreen)
        {
            DialogueUIManager.instance.Interact(inTrigger);
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
