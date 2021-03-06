﻿using UnityEngine;
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
    [SerializeField] GameObject saveConfirmPanel;
    [SerializeField] GameObject loadConfirmPanel;
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

    [Header("Save and Load")]
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;

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

    public void SelectButton(Button button)
    {
        button.Select();
        button.OnSelect(null);
    }

    void Pause()
    {
        paused = true;
        pausePanel.SetActive(true);
        RandomBugFact();
        UpdateNumberOfBugsLeft();
        PlayerMovement.instance.canMove = false;
        SelectButton(leftButtons[0]);

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
        SelectButton(waypointButtons[0]);

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

        SelectButton(mapButton);
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
