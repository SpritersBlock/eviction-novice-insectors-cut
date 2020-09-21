using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;

    public void GoToScene(string sceneToGoTo)
    {
        SceneTransition.instance.CallTransitionCoroutineWithSpecificMask(sceneToGoTo, -1);
    }

    public void ActivateMenu(int activeMenuIndex)
    {
        for (int i = 0; i < menuObjects.Length; i++)
        {
            if (i != activeMenuIndex)
            {
                menuObjects[i].SetActive(false);
            }
            else
            {
                menuObjects[i].SetActive(true);
            }
        }
    }

    public void SetSelectedObject(Button newSelection)
    {
        newSelection.Select();
        newSelection.OnSelect(null);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameFancy());
    }

    IEnumerator QuitGameFancy()
    {
        SceneTransition.instance.StartTransition();

        yield return new WaitForSeconds(1f);

        Application.Quit();

        yield return null;
    }
}
