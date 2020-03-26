using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void SetSelectedObject(GameObject newSelection)
    {
        EventSystem.current.SetSelectedGameObject(newSelection);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
