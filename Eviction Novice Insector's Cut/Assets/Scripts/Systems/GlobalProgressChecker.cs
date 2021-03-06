﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalProgressChecker : MonoBehaviour
{
    public static GlobalProgressChecker instance;
    public int bugsRemaining; //references NumberOfBugsLeft because refactoring is hard
    public string sceneName; //what scene the player is in

    public bool[] garden1ProgressCheck;
    //0,0 bee
    //0,1 peanut clock 1
    //0,2 peanut clock 2
    //0,3 flower 1
    //0,4 flower 2
    //0,5 snail
    //0,6 spider
    //0,7 boombox
    //0,8 collectable boombox
    //0,9 ant dad

    public bool[] garden2ProgressCheck;
    //1,0 silkworm
    //1,1 peanut clock 1
    //1,2 peanut clock 2
    //1,3 flower 1
    //1,4 flower 2

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void UpdateConditionallyActiveBool(int i, int o, bool newBool)
    {
        switch (i)
        {
            case 0:
                garden1ProgressCheck[o] = newBool;
                break;
            case 1:
                garden2ProgressCheck[o] = newBool;
                break;
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;
        bugsRemaining = NumberOfBugsLeft.instance.numberOfBugsLeft;

        if (scene.name == "Garden-1")
        {
            for (int i = 0; i < garden1ProgressCheck.Length; i++)
            {
                SceneProgressChecker.instance.UpdateSceneProgress(i, garden1ProgressCheck[i]);
            }
        }
        else if (scene.name == "Garden-2")
        {
            for (int i = 0; i < garden2ProgressCheck.Length; i++)
            {
                SceneProgressChecker.instance.UpdateSceneProgress(i, garden2ProgressCheck[i]);
            }
        }
    }
}
