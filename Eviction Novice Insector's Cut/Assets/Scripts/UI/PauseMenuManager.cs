using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Diagnostics;
using DG.Tweening;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components And Stuff")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] bool paused;
    public static PauseMenuManager instance;

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
            if (PlayerMovement.instance.canMove && !paused)
            {
                Pause();
            }
            else if (paused)// && !currentlyInMapSelection)
            {
                Unpause();            
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentlyInMapSelection)
            {
                ExitMapSelection();
            }
            else
            {
                Unpause();
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
        pausePanel.transform.localPosition = new Vector3(0, 300);
        pausePanel.transform.DOLocalMoveY(0, .5f).SetEase(Ease.OutExpo);
    }

    public void Unpause()
    {
        PlayerMovement.instance.canMove = true;
        paused = false;

        pausePanel.transform.DOKill();
        pausePanel.transform.localPosition = new Vector3(0, 0);
        pausePanel.transform.DOLocalMoveY(300, .3f).SetEase(Ease.InBack).OnComplete(DeactivatePausePanel);
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

    public void SetLastSelectedLeftButton(Button lastSelectedButton)
    {
        Navigation newNavi = new Navigation();
        newNavi.mode = Navigation.Mode.Explicit;
        lastSelectedLeftButton = lastSelectedButton;
        newNavi.selectOnLeft = lastSelectedLeftButton;
        mapButton.navigation = newNavi;
    }
}
