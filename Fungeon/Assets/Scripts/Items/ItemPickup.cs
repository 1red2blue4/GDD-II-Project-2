using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour //KI
{
    public AudioSource[] soundEffects;

    void Start() //Use this for initialization
    {
        soundEffects = new AudioSource[5];
        soundEffects = GetComponents<AudioSource>();
    }

    void Update() //Update is called once per frame
    {

    }

    void OnTriggerStay2D(Collider2D that) //If the player collides with something
    {
        if(that.gameObject.tag == "item") //If the player is colliding with the item
        {
            Destroy(that.gameObject); //Destroy the item
            soundEffects[4].Play();
        }
    }
}
