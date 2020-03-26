using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components And Stuff")]
    [SerializeField] GameObject pausePanel;

    [Header("Buttons On Left")]
    [SerializeField] Button[] leftButtons;

    [Header("Map")]
    [SerializeField] Button mapButton;
    [SerializeField] Button[] waypointButtons;
    bool currentlyInMapSelection;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(leftButtons[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentlyInMapSelection)
            {
                ExitMapSelection();
            }
            else
            {
                //exit map screen
            }
        }
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
}
