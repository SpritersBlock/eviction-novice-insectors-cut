using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components And Stuff")]
    [SerializeField] GameObject pausePanel;
    bool paused;
    public static PauseMenuManager instance;

    [Header("Buttons On Left")]
    [SerializeField] Button[] leftButtons;

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
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentlyInMapSelection)
            {
                ExitMapSelection();
            }
            else
            {
                //exit map screen
                Unpause();
            }
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        RandomBugFact();
        UpdateNumberOfBugsLeft();
        PlayerMovement.instance.canMove = false;
    }

    public void Unpause()
    {
        pausePanel.SetActive(false);
        PlayerMovement.instance.canMove = true;
    }

    public void EnterMapSelection() //The map button is already disabled so no need to do that here
    {
        //EVENTUALLY SET AUTOMATIC BUTTON SELECTION TO AREA YOU'RE IN BUT UNTIL THEN
        EventSystem.current.SetSelectedGameObject(waypointButtons[0].gameObject);

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

        EventSystem.current.SetSelectedGameObject(mapButton.gameObject);
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
}
