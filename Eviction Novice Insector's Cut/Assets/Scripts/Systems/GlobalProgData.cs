using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalProgData
{
    public string currentScene;
    public int bugsRemaining;
    public bool[] garden1Progress;
    public bool[] garden2Progress;

    public GlobalProgData(GlobalProgressChecker gpc)
    {
        currentScene = gpc.sceneName;
        bugsRemaining = gpc.bugsRemaining;

        garden1Progress = new bool[gpc.garden1ProgressCheck.Length];
        for (int i = 0; i < gpc.garden1ProgressCheck.Length; i++)
        {
            garden1Progress[i] = gpc.garden1ProgressCheck[i];
        }

        garden2Progress = new bool[gpc.garden2ProgressCheck.Length];
        for (int i = 0; i < gpc.garden2ProgressCheck.Length; i++)
        {
            garden2Progress[i] = gpc.garden2ProgressCheck[i];
        }
    }
}
