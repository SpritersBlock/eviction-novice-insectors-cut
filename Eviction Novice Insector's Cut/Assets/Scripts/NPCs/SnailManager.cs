using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailManager : MonoBehaviour
{
    [SerializeField] GameObject talkingClone;
    [SerializeField] GameObject movingClone;

    public void MakeSnailStartMoving()
    {
        movingClone.SetActive(true);
        talkingClone.SetActive(false);
        PlayerVIDE.instance.inTrigger = null;
    }
}
