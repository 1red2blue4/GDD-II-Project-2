using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Reloading scenes

public class UIOverlayScript : MonoBehaviour //KI
{
    [SerializeField] private Canvas UIOverlay; //The UI overlay
    [SerializeField] private HealthTracker playerHealthTracker; //The player's health
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    private UnityEngine.UI.Image[] UIImages; //The UI images for the player's health

    void Start() //Use this for initialization
    {
        UIImages = UIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children
    }
	
	void Update() //Update is called once per frame
    {
        healthUIUpdater(); //Update the player's health UI
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

    private void weaponUIUpdater() //Updates the player's weapon UI
    {
        for (int i = 0; i < playerControlScript.weapons.Length; i++) //For each weapon
        {
            if (playerControlScript.activeWeapon == i) //If the player's active weapon is the one to be enabled
            {
                UIImages[6 + i].enabled = true; //Enable the active weapon

                if (5 + i > 5) //If the weapon to the left exists
                {
                    UIImages[5 + i].enabled = false; //Disable the weapon to the left
                }
                else if (5 + i <= 5) //If the weapon to the left does not exist
                {
                    UIImages[UIImages.Length - 1].enabled = false; //Disable the weapon to the left
                }
                else if (7 + i <= UIImages.Length - 1) //If the weapon to the right exists
                {
                    UIImages[7 + i].enabled = false; //Disable the weapon to the right
                }
                else //If the weapon to the right does not exist
                {
                    UIImages[6].enabled = false; //Disable the weapon to the right
                }
            }
        }
    }
}