using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class HealthTracker : MonoBehaviour //KI
    {
        private int health; //Huebert's health, not his peril
        private float damageCooldown; //The time between when Huebert can take damage
        private SpriteRenderer playerSR; //The player's sprite renderer
        public AudioSource[] soundEffects;

        public int Health //Health property
        {
            get
            {
                return health; //Return the player's health
            }
            set
            {
                health = value; //Set the player's health
            }
        }

        void Start() //Use this for initialization
        {
            soundEffects = this.GetComponents<AudioSource>();
            health = 5; //Give Huebert 5 health
            damageCooldown = 1; //Set the damageCooldown
            playerSR = gameObject.GetComponentInChildren<SpriteRenderer>(); //Get the player's sprite renderer
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
            if (that.gameObject.tag == "enemy" && damageCooldown <= 0f && that.gameObject.name != "Collider") //If the player collides with an enemy
            {
                Enemy e = that.gameObject.GetComponent<Enemy>();
                health -= e.damage; //Decrement Huebert's health
                soundEffects[0].Play();
                //getsHit.Play();
                damageCooldown = 1; //Reset the damage cooldown
            }
        }

        private void blink() //Make Huebert blink while in cooldown
        {
            if (damageCooldown >= 0f) //If Huebert is in cooldown
            {
                if (playerSR.enabled) //If Huebert's sprite is on
                {
                    playerSR.enabled = false; //Turn off Huebert's sprite
                }
                else //If Huebert's sprite is off
                {
                    playerSR.enabled = true; //Turn on Huebert's sprite
                }
            }
            else //If Huebert is not in cooldown
            {
                playerSR.enabled = true; //Turn on Huebert's sprite
            }
        }
    }
}