using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Sprite[] maskCollection;
    [SerializeField] SVGImage mask;
    [SerializeField] Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CallTransitionCoroutine("hi");
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

        yield return new WaitForSeconds(1);

        anim.SetBool("TransitioningScenes", false);

        yield return null;
    }
}
