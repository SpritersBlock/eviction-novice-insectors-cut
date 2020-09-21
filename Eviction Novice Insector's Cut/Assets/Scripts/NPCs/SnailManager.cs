using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailManager : MonoBehaviour
{
    [SerializeField] GameObject talkingClone;
    [SerializeField] GameObject movingClone;

    [SerializeField] Vector2 progressNumber;

    public void MakeSnailStartMoving()
    {
        GlobalProgressChecker.instance.UpdateConditionallyActiveBool(Mathf.RoundToInt(progressNumber.x), Mathf.RoundToInt(progressNumber.y), false);

        movingClone.SetActive(true);
        talkingClone.SetActive(false);
        NumberOfBugsLeft.instance.RemoveOneBug();
        PlayerVIDE.instance.inTrigger = null;
    }
}
