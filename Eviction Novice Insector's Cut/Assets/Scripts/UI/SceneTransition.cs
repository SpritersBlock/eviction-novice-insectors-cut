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
            CallTransitionCoroutine("Garden-1");
        }
    }

    public void CallTransitionCoroutine(string sceneToMoveTo)
    {
        StartCoroutine(SceneTransitionCoroutine(sceneToMoveTo));
    }

    IEnumerator SceneTransitionCoroutine(string sceneToMoveTo)
    {
        mask.sprite = maskCollection[Random.Range(0, maskCollection.Length)];
        anim.SetBool("TransitioningScenes", true);

        yield return new WaitForSeconds(transitionAnimClip.length);
        SceneManager.LoadScene(sceneToMoveTo);

        anim.SetBool("TransitioningScenes", false);

        yield return null;
    }
}
