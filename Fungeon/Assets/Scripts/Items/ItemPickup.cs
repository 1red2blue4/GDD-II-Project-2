using UnityEngine;
using System.Collections;
using System.Collections.Generic; //List

public class ItemPickup : MonoBehaviour //KI
{
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    private int healthInventory; //Create an array to hold the health items
    private List<string> orbInventory = new List<string>(); //Create a list to hold the orb items
    public AudioSource[] soundEffects;

    void Start() //Use this for initialization
    {
        healthInventory = 0; //Initialize the player's health inventory
        soundEffects = new AudioSource[5];
        soundEffects = GetComponents<AudioSource>();
    }

    public int HealthInventory //HealthInventory property
    {
        get
        {
            return healthInventory; //Return the healthInventory
        }
        set
        {
            healthInventory = value; //Set the healthInventory value
        }
    }

    void OnTriggerEnter2D(Collider2D that) //If the player collides with something
    {
        if(that.gameObject.tag == "item" && playerControlScript.CanAttack == true) //If the player is colliding with the item and is not attacking
        {
            if (that.gameObject.name.Contains("HealthPickup") && healthInventory < 3) //If the item is a health pickup and there is no health in the last health inventory slot
            {
                healthInventory++; //Add a health pickup to the player's health inventory
            }
            else if (that.gameObject.name.Contains("Orb") && orbInventory.Count < 6) //If the item is an orb and there is no orb in the last orb inventory slot
            {
                orbInventory.Add(that.gameObject.name); //Add the orb to the list
            }

            Destroy(that.gameObject); //Destroy the item
            soundEffects[4].Play(); //Play a sound effect for destroying the item
        }
    }
}
