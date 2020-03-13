﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Sprite[] maskCollection;
    [SerializeField] SVGImage mask;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip transitionAnimClip;

    public static SceneTransition instance;

    private void Awake()
    {
        //if (instance == null)
        //{
            instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CallTransitionCoroutine("Garden-1", -1);
        }
    }

    public void CallTransitionCoroutine(string sceneToMoveTo, int specificMaskIndexToUse)
    {
        StartCoroutine(SceneTransitionCoroutine(sceneToMoveTo, specificMaskIndexToUse));
    }

    IEnumerator SceneTransitionCoroutine(string sceneToMoveTo, int specificMaskIndexToUse)
    {
        if (specificMaskIndexToUse < 0)
        {
            mask.sprite = maskCollection[Random.Range(0, maskCollection.Length)];
        }
        else
        {
            if (specificMaskIndexToUse < maskCollection.Length - 1)
            {
                mask.sprite = maskCollection[specificMaskIndexToUse];
            }
            else
            {
                Debug.LogWarning("Mask index is too high!");
                mask.sprite = maskCollection[Random.Range(0, maskCollection.Length)];
            }
        }
        anim.SetBool("TransitioningScenes", true);

        yield return new WaitForSeconds(transitionAnimClip.length);
        SceneManager.LoadScene(sceneToMoveTo);

        anim.SetBool("TransitioningScenes", false);

        yield return null;
    }
}
