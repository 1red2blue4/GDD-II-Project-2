using UnityEngine;
using System.Collections;
using System.Collections.Generic; //List

public class ItemPickup : MonoBehaviour //KI
{
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    private int healthInventory; //Create an array to hold the health items
    private List<string> orbInventory = new List<string>(); //Create a list to hold the orb items
    private bool[] activatedOrbEffect = new bool[6]; //If the orb's effect has been activated
    public AudioSource[] soundEffects;

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

    public List<string> OrbInventory //OrbInventory property
    {
        get
        {
            return orbInventory; //Return the orbInventory
        }
    }

    public bool []ActivatedOrbEffect //ActivatedOrbEffect property
    {
        get
        {
            return activatedOrbEffect; //Return the activatedOrbEffect
        }
    }

    void Start() //Use this for initialization
    {
        healthInventory = 0; //Initialize the player's health inventory
        orbInventory.Capacity = 6; //Set the capacity of the orb inventory
        soundEffects = new AudioSource[5];
        soundEffects = GetComponents<AudioSource>();

        for (int i = 0; i < activatedOrbEffect.Length; i++) //For each orb
        {
            activatedOrbEffect[i] = false; //Set the orb as inactive
        }
    }

    void OnTriggerEnter2D(Collider2D that) //If the player collides with something
    {
        if(that.gameObject.tag == "item" && playerControlScript.CanAttack == true) //If the player is colliding with the item and is not attacking
        {
            if (that.gameObject.name.Contains("HealthPickup") && healthInventory < 3) //If the item is a health pickup and there is no health in the last health inventory slot
            {
                healthInventory++; //Add a health pickup to the player's health inventory
                soundEffects[4].Play(); //Play a sound effect for destroying the item
            }
            else if (that.gameObject.name.Contains("Orb") && !orbInventory.Contains(that.gameObject.name)) //If the item is an orb and that orb is not already in the inventory
            {
                orbInventory.Add(that.gameObject.name); //Add the orb to the list
                soundEffects[4].Play(); //Play a sound effect for destroying the item
            }

            Destroy(that.gameObject); //Destroy the item
        }
    }
}
