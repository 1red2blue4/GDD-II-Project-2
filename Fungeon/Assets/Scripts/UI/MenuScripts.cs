using UnityEngine;
using System; //Exceptions
using System.Collections;
using UnityEngine.SceneManagement; //Load scenes
using UnityEngine.EventSystems; //Event systems

namespace UnityStandardAssets._2D
{
    public class MenuScripts : MonoBehaviour //KI
    {
        [SerializeField] private Canvas pauseMenu; //The pause menu UI
        [SerializeField] private Canvas keyboardControlMenu; //The keyboard controls menu UI
        [SerializeField] private Canvas controllerControlMenu; //The controller controls menu UI
        [SerializeField] private UIOverlayScript uIOverlay; //The player's health
        private bool controllerConnected; //If a controller is connected

        void Update() //Update is called once per frame
        {
            if (Input.GetButtonDown("Pause") && !keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy/* && !inventoryMenu.gameObject.activeInHierarchy*/) //If the pause button is pressed and the control menu is not active and the inventory menu is not active
            {
                Pause(); //Pause or unpause the game
            }
            else if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) && pauseMenu.gameObject.activeInHierarchy && !keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy/* && !inventoryMenu.gameObject.activeInHierarchy*/) //If the pause or cancel button is pressed while the pause menu is active and the control menu is not active and the inventory menu is not active
            {
                Pause(); //Pause or unpause the game
            }

            if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel")) && !pauseMenu.gameObject.activeInHierarchy && (keyboardControlMenu.gameObject.activeInHierarchy || controllerControlMenu.gameObject.activeInHierarchy)) //Boot from a control menu to the pause menu using the back or cancel buttons
            {
                keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
                controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
                pauseMenu.gameObject.SetActive(true); //Enable the pause menu
            }

            if (Input.GetButtonDown("Heal")) //If the heal button is pressed
            {
                Heal(); //Heal our savior Huebert
            }

            if (pauseMenu.gameObject.activeInHierarchy || keyboardControlMenu.gameObject.activeInHierarchy || controllerControlMenu.gameObject.activeInHierarchy) //If the pause menu is pressed
            {
                ControllerConnectionManager(); //Manages controller connection behavior
            }
        }

        private void Heal() //Heal our savior Huebert
        {
            if (uIOverlay.PlayerHealthTracker.Health < 5 && uIOverlay.PlayerInventory.HealthInventory > 0) //If the player's health is not at it's maximum and the player has health pickups
            {
                uIOverlay.PlayerHealthTracker.Health++; //Heal the player
                uIOverlay.PlayerInventory.HealthInventory--; //Use a health pickup
            }
        }

        private void Pause() //When the pause button is pressed
        {
            if (!pauseMenu.gameObject.activeInHierarchy) //If the pause menu is disabled
            {
                pauseMenu.gameObject.SetActive(true); //Enable the pause menu
                Time.timeScale = 0; //Pause the game
                uIOverlay.PlayerControlScript.M_Grounded = false; //Make the player not jump
            }
            else //If the pause menu is enabled
            {
                pauseMenu.gameObject.SetActive(false); //Disable the pause menu
                Time.timeScale = 1; //Run the game
            }
        }

        public void StartButton(string loadLevel) //When the start button is clicked
        {
            SceneManager.LoadScene(loadLevel); //Load the scene
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
            if (!keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy) //If the keyboard controls menu is disabled and the controller controls menu is disabled
            {
                if (!controllerConnected) //If there is no controller connected
                {
                    keyboardControlMenu.gameObject.SetActive(true); //Enable the keyboard controls menu
                    pauseMenu.gameObject.SetActive(false); //Disable the pause menu
                }
                else //If there is a controller connected
                {
                    controllerControlMenu.gameObject.SetActive(true); //Enable the controller controls menu
                    pauseMenu.gameObject.SetActive(false); //Disable the pause menu
                }
            }
            else //In any other case
            {
                keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
                controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
                pauseMenu.gameObject.SetActive(true); //Enable the pause menu
            }
        }

        private void ControllerConnectionManager() //Manages controller connection behavior
        {
            try //Try to do this
            {
                if (Input.GetJoystickNames()[0] == "") //If there is not a controller connected
                {
                    controllerConnected = false; //There is no controller connected
                }
                else //If there is a controller connected
                {
                    controllerConnected = true; //There is a controller connected
                }
            }
            catch (IndexOutOfRangeException) //If the game is starting while no controller is connected
            {
                controllerConnected = false; //There is no controller connected
            }

            if (controllerConnected && keyboardControlMenu.gameObject.activeInHierarchy && !controllerControlMenu.gameObject.activeInHierarchy) //If a controller is connected while the keyboard controls menu is enabled and the controller controls menu is disabled
            {
                keyboardControlMenu.gameObject.SetActive(false); //Disable the keyboard controls menu
                controllerControlMenu.gameObject.SetActive(true); //Enable the controller controls menu
            }
            else if (!controllerConnected && !keyboardControlMenu.gameObject.activeInHierarchy && controllerControlMenu.gameObject.activeInHierarchy) //If a controller is disconnected while the keyboard controls menu is disabled and the controller controls menu is enabled
            {
                keyboardControlMenu.gameObject.SetActive(true); //Enable the keyboard controls menu
                controllerControlMenu.gameObject.SetActive(false); //Disable the controller controls menu
            }
        }
    }
}