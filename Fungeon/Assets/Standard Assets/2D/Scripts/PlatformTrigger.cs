using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour //KI
{
    private PlatformTransform pT; //PlatformTransform script

	void Start() //Use this for initialization
    {
        pT = GetComponentInParent<PlatformTransform>(); //Get the parent PlatformTransform script
	}
	
	void Update() //Update is called once per frame
    {

	}

    void OnCollisionStay2D(Collision2D that) //Detect collisions with the collider
    {
        if (that.gameObject.tag == "Player") //If the colliding object is the player
        {
            if (pT.m_Position && pT.m_Sticky) //If the player should stick to the platform when it moves
            {
                that.transform.position = new Vector3(transform.position.x, transform.position.y + transform.parent.transform.localScale.y, that.transform.position.z); //Snap the player to the collider
            }
            if (pT.m_Position && pT.m_PositionIsTrigger) //If the collider has a position trigger
            {
                pT.m_PositionIsTrigger = false; //Activate the position trigger
                pT.Timer = 0; //Reset the timer
            }

            if (pT.m_Rotation && (Mathf.Abs(pT.transform.eulerAngles.z) <= 70f || Mathf.Abs(pT.transform.eulerAngles.z) >= 290f)) //If the platform is rotating at 70 degrees
            {
                that.transform.rotation = transform.rotation; //Rotate the player with the platform
            }
            if (pT.m_Rotation && pT.m_RotationIsTrigger) //If collider has a rotation trigger
            {
                pT.m_RotationIsTrigger = false; //Activate the rotation trigger
            }
        }
    }

    void OnCollisionExit2D(Collision2D that) //Detect when leaving the collider
    {
        if (that.gameObject.tag == "Player" && pT.m_Rotation) //If the colliding object is the player and the platform is rotating
        {
            that.transform.rotation = Quaternion.identity; //Reset the rotation of the player
        }
    }
}