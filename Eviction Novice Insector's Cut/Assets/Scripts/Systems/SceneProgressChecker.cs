using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneProgressChecker : MonoBehaviour
{
    public static SceneProgressChecker instance;

    [SerializeField] ConditionallyActive[] conditionalComponents;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateSceneProgress(int index, bool newState)
    {
        conditionalComponents[index].UpdateActiveness(newState);
    }
}
