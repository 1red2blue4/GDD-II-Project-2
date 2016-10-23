using UnityEngine;
using System.Collections;
using System.Collections.Generic; //List

public class ItemPickup : MonoBehaviour //KI
{
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D playerControlScript; //The player's control script 
    private List<string> inventory = new List<string>(); //Create a list to hold the items

    void OnTriggerStay2D(Collider2D that) //If the player collides with something
    {
        if(that.gameObject.tag == "item" && playerControlScript.CanAttack == true) //If the player is colliding with the item
        {
            if (inventory.Count < 10) //If there is no item in the last inventory slot
            {
                inventory.Add(that.gameObject.name); //Add the item to the list
            }

            Destroy(that.gameObject); //Destroy the item
        }
    }
}