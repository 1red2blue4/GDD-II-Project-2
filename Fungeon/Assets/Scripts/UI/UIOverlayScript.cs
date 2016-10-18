using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Reloading scenes

public class UIOverlayScript : MonoBehaviour //KI
{
    [SerializeField] private Canvas UIOverlay; //The UI overlay
    [SerializeField] private HealthTracker playerHealthTracker; //The player
    private UnityEngine.UI.Image[] healthUIImages; //The UI images for the player's health

    void Start() //Use this for initialization
    {
        healthUIImages = UIOverlay.GetComponentsInChildren<UnityEngine.UI.Image>(); //Get the images from the children
    }
	
	void Update() //Update is called once per frame
    {
        if (playerHealthTracker.Health == 3) //If the player has full health
        {
            healthUIImages[0].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
            healthUIImages[1].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
            healthUIImages[2].color = new Color(0, .6f, 0, 1); //Change the color of the sprites
        }
        else if (playerHealthTracker.Health == 2) //If the player has 2 health
        {
            healthUIImages[0].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
            healthUIImages[1].color = new Color(.6f, .6f, 0, 1); //Change the color of the sprites
            healthUIImages[2].color = new Color(0, 0, 0, 0); //Change the color of the sprites
        }
        else if (playerHealthTracker.Health == 1) //If the player has 1 health
        {
            healthUIImages[0].color = new Color(.6f, 0, 0, 1); //Change the color of the sprites
            healthUIImages[1].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            healthUIImages[2].color = new Color(0, 0, 0, 0); //Change the color of the sprites
        }
        else //If the player has no health
        {
            healthUIImages[0].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            healthUIImages[1].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            healthUIImages[2].color = new Color(0, 0, 0, 0); //Change the color of the sprites
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name); //Reload the scene
        }
    }
}