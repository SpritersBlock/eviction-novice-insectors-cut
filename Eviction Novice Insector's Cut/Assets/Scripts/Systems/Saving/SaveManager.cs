using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
        //GLOBAL
        SaveSystem.SaveGlobalProgress(GlobalProgressChecker.instance);

        //DIALOGUE
        VD.SaveState("Garden", true);
    }

    public void LoadGame()
    {
        //GLOBAL
        GlobalProgData data = SaveSystem.LoadGlobalProgress();

        SceneTransition.instance.CallTransitionCoroutineRandomMask(data.currentScene);
        NumberOfBugsLeft.instance.numberOfBugsLeft = data.bugsRemaining; //BROKEN. i think there's a weird total - remaining thing i need to do

        for (int i = 0; i < data.garden1Progress.Length; i++)
        {
            GlobalProgressChecker.instance.garden1ProgressCheck[i] = data.garden1Progress[i];
        }
        for (int i = 0; i < data.garden2Progress.Length; i++)
        {
            GlobalProgressChecker.instance.garden2ProgressCheck[i] = data.garden2Progress[i];
        }

        //DIALOGUE
        VD.LoadState("Garden", true);
    }

    public void ResetGame()
    {
        //GLOBAL
        for (int i = 0; i < GlobalProgressChecker.instance.garden1ProgressCheck.Length; i++)
        {
            GlobalProgressChecker.instance.garden1ProgressCheck[i] = true;
        }
        for (int i = 0; i < GlobalProgressChecker.instance.garden2ProgressCheck.Length; i++)
        {
            GlobalProgressChecker.instance.garden2ProgressCheck[i] = true;
        }

        //DIALOGUE

        SceneTransition.instance.CallTransitionCoroutineRandomMask("Garden-1");
    }
}
