using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Reloading scenes

namespace UnityStandardAssets._2D
{
    public class UIOverlayScript : MonoBehaviour //KI
    {
        [SerializeField] private Canvas UIOverlay; //The UI overlay
        [SerializeField] private HealthTracker playerHealthTracker; //The player's health
        [SerializeField] private ItemPickup playerInventory; //The player's inventory script
        [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
        private UnityEngine.UI.Image[] UIImages; //The UI images for the player's health
        private bool orbInventoryFull; //If the player's orb inventory is full

        public HealthTracker PlayerHealthTracker //PlayerHealthTracker property
        {
            get
            {
                return playerHealthTracker; //Return the player's health tracker
            }
        }

        public UnityStandardAssets._2D.PlatformerCharacter2D PlayerControlScript //PlayerControlScript property
        {
            get
            {
                return playerControlScript; //Return the player's playerControlScript
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
            orbInventoryFull = false; //The player's orb inventory is not full
        }

        void Update() //Update is called once per frame
        {
            HealthUIUpdater(); //Update the player's health UI
            HealthRestoreUIUpdater(); //Update the player's health restore UI
            OrbUIUpdater(); //Update the player's orb UI
            WeaponUIUpdater(); //Update the player's weapon UI
        }

        private void HealthUIUpdater() //Updates the player's health UI
        {
            if (playerHealthTracker.Health == 5) //If the player has full health
            {
                UIImages[0].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[1].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[3].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[5].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[7].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[9].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 4) //If the player has 4 health
            {
                UIImages[0].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[1].color = new Color(.6f, 1, 0, 1); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[3].color = new Color(.6f, 1, 0, 1); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[5].color = new Color(.6f, 1, 0, 1); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[7].color = new Color(.6f, 1, 0, 1); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[9].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 3) //If the player has 3 health
            {
                UIImages[0].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[1].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[3].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[5].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[7].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[9].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 2) //If the player has 2 health
            {
                UIImages[0].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[1].color = new Color(1, .6f, 0, 1); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[3].color = new Color(1, .6f, 0, 1); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[7].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[9].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else if (playerHealthTracker.Health == 1) //If the player has 1 health
            {
                UIImages[0].color = new Color(1, 1, 1, 1); //Change the color of the background sprites
                UIImages[1].color = new Color(.6f, 0, 0, 1); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[3].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[7].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[9].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            }
            else //If the player has no health
            {
                UIImages[0].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[1].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[2].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[3].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[4].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[5].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[6].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[7].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                UIImages[8].color = new Color(1, 1, 1, 0); //Change the color of the background sprites
                UIImages[9].color = new Color(0, 0, 0, 0); //Change the color of the sprites
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name); //Reload the scene
            }
        }

        private void HealthRestoreUIUpdater() //Updates the player's health restore UI
        {
            if (playerInventory.HealthInventory == 0) //If the player has no health pickups
            {
                UIImages[10].enabled = false; //Show the health pickup on the UI
                UIImages[11].enabled = false; //Hide the health pickup on the UI
                UIImages[12].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 1) //If the player has one health pickup
            {
                UIImages[10].enabled = true; //Show the health pickup on the UI
                UIImages[11].enabled = false; //Hide the health pickup on the UI
                UIImages[12].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 2) //If the player has two health pickups
            {
                UIImages[10].enabled = true; //Show the health pickup on the UI
                UIImages[11].enabled = true; //Show the health pickup on the UI
                UIImages[12].enabled = false; //Hide the health pickup on the UI
            }
            else if (playerInventory.HealthInventory == 3) //If the player has three health pickups
            {
                UIImages[10].enabled = true; //Show the health pickup on the UI
                UIImages[11].enabled = true; //Show the health pickup on the UI
                UIImages[12].enabled = true; //Show the health pickup on the UI
            }
        }

        private void OrbUIUpdater() //Updates the player's orb UI
        {
            if (!orbInventoryFull) //If the player's orb inventory is not full
            {
                for (int i = 0; i < playerInventory.OrbInventory.Capacity; i++) //For each orb in the orb inventory
                {
                    if (playerInventory.OrbInventory.Contains(UIImages[13 + i].name)) //If the orb inventory contains the orb
                    {
                        UIImages[13 + i].color = new Color(UIImages[13 + i].color.r, UIImages[13 + i].color.g, UIImages[13 + i].color.b, 1); //Make the orb's UI less transparent
                    }

                    if (playerInventory.OrbInventory.Count == playerInventory.OrbInventory.Capacity) //If the player's orb inventory is ful
                    {
                        orbInventoryFull = true; //The player's orb inventory is full
                    }
                }
            }
        }

        private void WeaponUIUpdater() //Updates the player's weapon UI
        {
            for (int i = 0; i < playerControlScript.weapons.Length; i++) //For each weapon
            {
                if (playerControlScript.activeWeapon == i) //If the player's active weapon is the one to be enabled
                {
                    UIImages[19 + i].enabled = true; //Enable the active weapon

                    if (18 + i > 18) //If the weapon to the left exists
                    {
                        UIImages[18 + i].enabled = false; //Disable the weapon to the left
                    }
                    else if (18 + i <= 18) //If the weapon to the left does not exist
                    {
                        UIImages[UIImages.Length - 1].enabled = false; //Disable the weapon to the left
                    }
                    else if (20 + i <= UIImages.Length - 1) //If the weapon to the right exists
                    {
                        UIImages[20 + i].enabled = false; //Disable the weapon to the right
                    }
                    else //If the weapon to the right does not exist
                    {
                        UIImages[15].enabled = false; //Disable the weapon to the right
                    }
                }
            }
        }
    }
}