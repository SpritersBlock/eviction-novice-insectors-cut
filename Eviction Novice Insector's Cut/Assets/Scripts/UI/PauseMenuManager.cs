using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using VIDE_Data;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components And Stuff")]
    [SerializeField] GameObject pausePanel;
    public bool paused;
    public static PauseMenuManager instance;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] MenuManager menuManager;

    [Header("Buttons On Left")]
    [SerializeField] Button[] leftButtons;
    Button lastSelectedLeftButton;

    [Header("Map")]
    [SerializeField] Button mapButton;
    [SerializeField] Button[] waypointButtons;
    bool currentlyInMapSelection;

    [Header("Bug Facts")]
    [SerializeField] string[] bugFacts;
    [SerializeField] TextMeshProUGUI bugFactText;

    [Header("Bugs Left")]
    [SerializeField] Slider numberOfBugsLeftSlider;
    [SerializeField] TextMeshProUGUI numberOfBugsLeftText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(leftButtons[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (PlayerMovement.instance.canMove && !paused && !VD.isActive)
            {
                Pause();
            }
            else if (paused && menuManager.menuObjects[0].activeSelf)
            {
                Unpause();            
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                if (currentlyInMapSelection)
                {
                    ExitMapSelection();
                }
                else
                {
                    menuManager.ActivateMenu(0);
                    Unpause();
                }
            }
        }
    }

    void Pause()
    {
        paused = true;
        pausePanel.SetActive(true);
        RandomBugFact();
        UpdateNumberOfBugsLeft();
        PlayerMovement.instance.canMove = false;
        leftButtons[0].Select();
        leftButtons[0].OnSelect(null);

        pausePanel.transform.DOKill();
        pausePanel.transform.localPosition = new Vector3(0, 800);
        pausePanel.transform.DOLocalMoveY(0, .4f).SetEase(Ease.OutExpo);
    }

    public void Unpause()
    {
        PlayerMovement.instance.canMove = true;
        paused = false;

        pausePanel.transform.DOKill();
        pausePanel.transform.localPosition = new Vector3(0, 0);
        pausePanel.transform.DOLocalMoveY(800, .6f).SetEase(Ease.InBack).OnComplete(DeactivatePausePanel);
    }

    void DeactivatePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void EnterMapSelection() //The map button is already disabled so no need to do that here
    {
        //EVENTUALLY SET AUTOMATIC BUTTON SELECTION TO AREA YOU'RE IN BUT UNTIL THEN
        waypointButtons[0].Select();
        waypointButtons[0].OnSelect(null);

        for (int i = 0; i < waypointButtons.Length; i++)
        {
            waypointButtons[i].enabled = true;
        }
        currentlyInMapSelection = true;
    }

    public void ExitMapSelection()
    {
        mapButton.enabled = true;
        for (int i = 0; i < waypointButtons.Length; i++)
        {
            waypointButtons[i].enabled = false;
        }
        currentlyInMapSelection = false;

        mapButton.Select();
        mapButton.OnSelect(null);
    }

    void RandomBugFact()
    {
        bugFactText.text = bugFacts[Random.Range(0, bugFacts.Length)];
    }

    void UpdateNumberOfBugsLeft()
    {
        if (numberOfBugsLeftSlider.maxValue != NumberOfBugsLeft.instance.numberOfBugsTotal)
        {
            numberOfBugsLeftSlider.maxValue = NumberOfBugsLeft.instance.numberOfBugsTotal;
        }
        numberOfBugsLeftSlider.value = NumberOfBugsLeft.instance.numberOfBugsTotal - NumberOfBugsLeft.instance.numberOfBugsLeft;

        numberOfBugsLeftText.text = NumberOfBugsLeft.instance.numberOfBugsLeft + "/" + NumberOfBugsLeft.instance.numberOfBugsTotal + "\n<size=-6>Bugs Left</size>";
    }

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
        NumberOfBugsLeft.instance.numberOfBugsLeft = data.bugsRemaining;

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

    public void SetLastSelectedLeftButton(Button lastSelectedButton)
    {
        Navigation newNavi = new Navigation();
        newNavi.mode = Navigation.Mode.Explicit;
        lastSelectedLeftButton = lastSelectedButton;
        newNavi.selectOnLeft = lastSelectedLeftButton;
        mapButton.navigation = newNavi;
    }
}
