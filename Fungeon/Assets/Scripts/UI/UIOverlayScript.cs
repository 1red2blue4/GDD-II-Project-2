using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Reloading scenes

namespace UnityStandardAssets._2D
{
    public class UIOverlayScript : MonoBehaviour //KI
    {
        [SerializeField]
        private Canvas UIOverlay; //The UI overlay
        [SerializeField]
        private HealthTracker playerHealthTracker; //The player's health
        [SerializeField]
        private ItemPickup playerInventory; //The player's inventory script
        [SerializeField]
        private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
        private UnityEngine.UI.Image[] UIImages; //The UI images for the player's health

        public HealthTracker PlayerHealthTracker //PlayerHealthTracker property
        {
            get
            {
                return playerHealthTracker; //Return the player's health tracker
            }
        }

        public ItemPickup PlayerInventory //PlayerInventory property
        {
            get
            {
                return playerInventory; //Return the player's inventory
            }
        }

        void Start() //Use this for initialization
        {
            UIImages = UIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children
        }

        void Update() //Update is called once per frame
        {
            healthUIUpdater(); //Update the player's health UI
            healthRestoreUIUpdater(); //Update the player's health restore UI
            weaponUIUpdater(); //Update the player's weapon UI
        }

        private void healthUIUpdater() //Updates the player's health UI
        {
            if (playerHealthTracker.Health == 3) //If the player has full health
            {
                UIImages[1].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[3].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[5].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 2) //If the player has 2 health
            {
                UIImages[1].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
                UIImages[3].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 1) //If the player has 1 health
            {
                UIImages[1].color = new Color(.6f, 0, 0, 1); //Change the color of the sprites
                UIImages[3].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else //If the player has no health
            {
                UIImages[1].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[3].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name); //Reload the scene
            }
        }

        private void healthRestoreUIUpdater() //Updates the player's health restore UI
        {
            if (playerInventory.HealthInventory == 0) //If the player has no health pickups
            {
                UIImages[6].enabled = false; //Show the health pickup on the UI
                UIImages[7].enabled = false; //Hide the health pickup on the UI
                UIImages[8].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 1) //If the player has one health pickup
            {
                UIImages[6].enabled = true; //Show the health pickup on the UI
                UIImages[7].enabled = false; //Hide the health pickup on the UI
                UIImages[8].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 2) //If the player has two health pickups
            {
                UIImages[7].enabled = true; //Show the health pickup on the UI
                UIImages[8].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 3) //If the player has three health pickups
            {
                UIImages[8].enabled = true; //Show the health pickup on the UI
            }
        }

        private void weaponUIUpdater() //Updates the player's weapon UI
        {
            for (int i = 0; i < playerControlScript.weapons.Length; i++) //For each weapon
            {
                UIImages[15 + i].enabled = true; //Enable the active weapon

                if (14 + i > 14) //If the weapon to the left exists
                {
                    UIImages[14 + i].enabled = false; //Disable the weapon to the left
                }
                else if (14 + i <= 14) //If the weapon to the left does not exist
                {
                    UIImages[UIImages.Length - 1].enabled = false; //Disable the weapon to the left
                }
                else if (16 + i <= UIImages.Length - 1) //If the weapon to the right exists
                {
                    UIImages[16 + i].enabled = false; //Disable the weapon to the right
                }
                else //If the weapon to the right does not exist
                {
                    UIImages[16].enabled = false; //Disable the weapon to the right
                }
            }
        }
    }
}