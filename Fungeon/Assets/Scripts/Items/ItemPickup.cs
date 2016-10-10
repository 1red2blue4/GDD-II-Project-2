using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour //KI
{
    void Start() //Use this for initialization
    {

    }

    void Update() //Update is called once per frame
    {

    }

    void OnTriggerStay2D(Collider2D that) //If the player collides with something
    {
        if(that.gameObject.tag == "item") //If the player is colliding with the item
        {
            Debug.Log("Collided with item");
        }
    }
}
