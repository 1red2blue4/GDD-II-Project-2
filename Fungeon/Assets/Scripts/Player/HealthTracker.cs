using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour //KI
{
    private int health; //Huebert's health, not his peril
    private float damageCooldown; //The time between when Huebert can take damage

    public int Health //Health property
    {
        get
        {
            return health; //Return the value of health
        }
    }

	void Start() //Use this for initialization
    {
        health = 3; //Give Huebert 3 health
        damageCooldown = 1; //Set the damageCooldown
	}
	
	void Update() //Update is called once per frame
    {
        if (damageCooldown > 0f) //If the cooldown between hits is less than two
        {
            damageCooldown -= Time.deltaTime; //Increment the cooldown timer
        }

        blink(); //Make Huebert blink
	}

    void OnTriggerStay2D(Collider2D that) //Detect when things enter the player's trigger
    {
        if (that.gameObject.tag == "enemy" && damageCooldown <= 0f) //If the player collides with an enemy
        {
            health--; //Decrement Huebert's health
            damageCooldown = 1; //Reset the damage cooldown
        }
    }

    void blink() //Make Huebert blink while in cooldown
    {
        if (damageCooldown >= 0f) //If Huebert is in cooldown
        {
            if (gameObject.GetComponentInChildren<SpriteRenderer>().enabled) //If Huebert's sprite is on
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false; //Turn off Huebert's sprite
            }
            else //If Huebert's sprite is off
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true; //Turn on Huebert's sprite
            }
        }
        else //If Huebert is not in cooldown
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true; //Turn on Huebert's sprite
        }
    }
}