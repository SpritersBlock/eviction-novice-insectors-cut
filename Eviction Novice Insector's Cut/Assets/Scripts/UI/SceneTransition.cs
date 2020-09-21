using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Sprite[] maskCollection;
    [SerializeField] SVGImage mask;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip transitionAnimClip;
    public bool transitioning;

    public static SceneTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetMask(-1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CallTransitionCoroutineWithSpecificMask("Garden-1", -1);
        }
    }

    public void CallTransitionCoroutineRandomMask(string sceneToMoveTo)
    {
        StartCoroutine(SceneTransitionCoroutine(sceneToMoveTo, -1));
    }

    public void CallTransitionCoroutineWithSpecificMask(string sceneToMoveTo, int specificMaskIndexToUse)
    {
        StartCoroutine(SceneTransitionCoroutine(sceneToMoveTo, specificMaskIndexToUse));
    }

    IEnumerator SceneTransitionCoroutine(string sceneToMoveTo, int specificMaskIndexToUse)
    {
        SetMask(specificMaskIndexToUse);

        //Doesn't work - I think it's being called before the dialogue system reactivates canMove.
        //if (PlayerMovement.instance)
        //{
        //    PlayerMovement.instance.canMove = false;
        //}
        transitioning = true;

        anim.SetBool("TransitioningScenes", true);

        yield return new WaitForSeconds(transitionAnimClip.length);
        SceneManager.LoadScene(sceneToMoveTo);

        anim.SetBool("TransitioningScenes", false);

        if (PlayerMovement.instance)
        {
            PlayerMovement.instance.canMove = false;
        }
        transitioning = false;
        yield return null;
    }

    void SetMask(int specificMaskIndexToUse)
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
    }

    public void StartTransition()
    {
        anim.SetBool("TransitioningScenes", true);
    }
}
