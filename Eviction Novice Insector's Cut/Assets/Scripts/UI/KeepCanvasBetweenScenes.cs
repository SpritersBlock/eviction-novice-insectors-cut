using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepCanvasBetweenScenes : MonoBehaviour
{
    public static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Loaded Scene: " + scene.name + ", Mode: " + mode);
        if (scene.name == "Title Screen")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
