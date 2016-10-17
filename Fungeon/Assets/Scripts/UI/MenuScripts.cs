using UnityEngine;
using UnityEditor.SceneManagement; //Load scenes
using System.Collections;

public class MenuScripts : MonoBehaviour //KI
{
    [SerializeField] private Canvas pauseMenu; //The pause menu UI
    [SerializeField] private Canvas controlMenu; //The controls menu UI

	void Update() //Update is called once per frame
    {
        MenuInputManager(); //Check for button presses
	}

    public void StartButton(string loadLevel) //When the start button is clicked
    {
        EditorSceneManager.LoadScene(loadLevel); //Load the scene
    }

    public void ResumeButton() //When the resume button is clicked
    {
        pauseMenu.gameObject.SetActive(false); //Disable the pause menu
        Time.timeScale = 1; //Run the game
    }

    public void QuitButton() //When the exit button is clicked
    {
        Application.Quit(); //Quit the game
    }

    public void ControlsButton() //When the controls button is clicked
    {
        if (!controlMenu.gameObject.activeInHierarchy) //If the controls menu is disabled
        {
            controlMenu.gameObject.SetActive(true); //Enable the controls menu
            pauseMenu.gameObject.SetActive(false); //Disable the pause menu
        }
        else  //If the controls menu is enabled
        {
            controlMenu.gameObject.SetActive(false); //Disable the controls menu
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
        }
    }

    public void MenuInputManager() //Manages button input
    {
        if (Input.GetButtonDown("Pause") && !controlMenu.gameObject.activeInHierarchy) //If the pause button is pressed and the control menu is not active
        {
            Pause(); //Pause or unpause the game
        }
        else if((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) && pauseMenu.gameObject.activeInHierarchy && !controlMenu.gameObject.activeInHierarchy) //If the pause or cancel button is pressed while the pause menu is active and the control menu is not active
        {
            Pause(); //Pause or unpause the game
        }

        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) //If the pause or cancel button is pressed
        {
            Controls(); //Bring up or close the controls menu
        }
    }

    public void Pause() //When the pause button is clicked
    {
        if (!pauseMenu.gameObject.activeInHierarchy) //If the pause menu is disabled
        {
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
            Time.timeScale = 0; //Pause the game
        }
        else //If the pause menu is enabled
        {
            pauseMenu.gameObject.SetActive(false); //Disable the pause menu
            Time.timeScale = 1; //Run the game
        }
    }

    public void Controls() //Handles closing the controls button with a keyboard command
    {
        if (!pauseMenu.gameObject.activeInHierarchy && controlMenu.gameObject.activeInHierarchy) //If the pause menu is disabled and the control menu is enabled
        {
            controlMenu.gameObject.SetActive(false); //Disable the controls menu
            pauseMenu.gameObject.SetActive(true); //Enable the pause menu
        }
    }
}