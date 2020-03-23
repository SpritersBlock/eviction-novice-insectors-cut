using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void GoToScene(string sceneToGoTo)
    {
        SceneTransition.instance.CallTransitionCoroutineWithSpecificMask(sceneToGoTo, 1);
    }
}
