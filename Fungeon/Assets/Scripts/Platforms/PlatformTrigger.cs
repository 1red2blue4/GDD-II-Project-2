using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour //KI
{
    private PlatformTransform pT; //PlatformTransform script

	void Start() //Use this for initialization
    {
        pT = GetComponentInParent<PlatformTransform>(); //Get the parent PlatformTransform script
	}

    void OnTriggerStay2D(Collider2D that) //Detect collisions with the trigger
    {
        if (that.gameObject.tag == "Player") //If the colliding object is the player
        {
            if ((pT.M_Position || pT.M_Circle) && pT.M_Sticky && !Input.GetButton("Jump") && Mathf.Abs(Input.GetAxis("Horizontal")) < .05f) //If the player should stick to the platform when it moves
            {
                that.transform.position = new Vector3(transform.position.x, transform.position.y + transform.parent.transform.localScale.y + .05f, that.transform.position.z); //Snap the player to the collider
            }
            if (pT.M_Position && pT.M_PositionIsTrigger) //If the collider has a position trigger
            {
                pT.M_PositionIsTrigger = false; //Activate the position trigger
                pT.Timer = 0; //Reset the timer
            }
    
            if (pT.M_Rotation && (Mathf.Abs(pT.transform.eulerAngles.z) <= 70f || Mathf.Abs(pT.transform.eulerAngles.z) >= 290f)) //If the platform is rotating at 70 degrees
            {
                that.transform.rotation = transform.rotation; //Rotate the player with the platform
            }
            if (pT.M_Rotation && pT.M_RotationIsTrigger) //If collider has a rotation trigger
            {
                pT.M_RotationIsTrigger = false; //Activate the rotation trigger
            }
    
            if(pT.M_Circle && pT.M_CircleIsTrigger) //If the collider has a circle trigger
            {
                pT.M_CircleIsTrigger = false; //Activate the circle trigger
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D that) //Detect when leaving the trigger
    {
        if (that.gameObject.tag == "Player" && pT.M_Rotation) //If the colliding object is the player and the platform is rotating
        {
            that.transform.rotation = Quaternion.identity; //Reset the rotation of the player
        }
    }
}