using UnityEngine;
using System.Collections;

public class PlatformTransform : MonoBehaviour //KI
{
    public bool m_Position = false; //Inspector checkbox for position transforms
    [Tooltip("When unchecked and moving in both the X and Y directions, movement speed will be averaged")] //m_LoopPosition tooltop
    public bool m_LoopPosition = false; //Inspector checkbox to loop the movement
    public bool m_PositionIsTrigger = false; //Inspector checkbox to make the object have a trigger
    public bool m_Sticky = false; //Inspector checkbox to make an object sticky
    //[SerializeField] private bool m_DiamondShape = false; //Inspector checkbox to move the platforms in a diamond shape
    //[SerializeField] private bool m_VShape = false; //Inspector checkbox to move the platforms in a V shape
    //[SerializeField] private bool m_InfinityShape = false; //Inspector checkbox to move the platforms in an infinity shape

    [Tooltip("Distance the object will move in the X direction. A negative value will make the object move left first")] //moveDistanceX tooltip
    public float moveDistanceX; //Distance to move the platform in the X direction
    [Tooltip("Distance the object will move in the Y direction. A negative value will make the object move down first")] //moveDistanceY tooltip
    public float moveDistanceY; //Distance to move the platform in the Y direction
    [Tooltip("Movement speed in the X direction")] //moveSpeedX tooltip
    public float moveSpeedX; //How fast the platform can move
    [Tooltip("Movement speed in the Y direction")] //moveSpeedY tooltip
    public float moveSpeedY; //How fast the platform can move
    [Space(10)] //Space out the inspector UI elements

    public bool m_Rotation = false; //Inspector checkbox for rotation transforms
    public bool m_RotationIsTrigger = false; //Inspector checkbox to make the object have a trigger

    [Tooltip("How many degrees that platform will rotate each second. A negative value will make the object rotate right")] //m_DegreesPerSecond tooltip
    public float m_DegreesPerSecond; //How many degrees to rotate the platform by each second
    [Space(10)] //Space out the inspector UI elements

    private SpriteRenderer rend; //Renderer for object sizes
    private Vector3 startPosition; //The starting position of the object
    private bool runOnce = false; //Controls the movement of the platform when it is not looping
    private float timer; //Timer for movement    

    public float Timer //Timer property
    {
        get
        {
            return timer; //Return the value of the timer
        }
        set
        {
            timer = value; //Set the timer equal to the value
        }
    }

    void Start() //Runs once to initialize
    {
        rend = GetComponentInChildren<SpriteRenderer>(); //Get the renderer

        if (m_Position) //If the toggle box for position transforms is checked
        {
            startPosition = transform.position; //Set the starting position
            Mathf.Abs(moveSpeedX); //Make the move speed positive for the X direction
            Mathf.Abs(moveSpeedY); //Make the move speed positive for the Y direction

            if (!m_LoopPosition && moveDistanceX != 0 && moveDistanceY != 0) //If the position is not looping
            {
                moveSpeedX = (moveSpeedX + moveSpeedY) / 2; //Average the movement speeds, set the movement speed in the X direction
                moveSpeedY = moveSpeedX; //Set the movement speed in the Y direction
            }
        }
    }

    void FixedUpdate() //FixedUpdate for better physics
    {
        if (m_Position) //If the toggle box for position transforms is checked
        {
            transformPosition(); //Transform the position
        }
        if (m_Rotation) //If the toggle box for rotation transforms is checked
        {
            transformRotation(); //Transform the rotation
        }

        timer += Time.deltaTime; //Set the timer
    }

    void transformPosition() //Moves the platform
    {
        if (!m_PositionIsTrigger) //Loop when not triggered
        {
            if (m_LoopPosition) //If the platform should loop
            {
                transform.position = startPosition + new Vector3(Mathf.Sin(timer * moveSpeedX) * moveDistanceX, Mathf.Sin(timer * moveSpeedY) * moveDistanceY, 0); //Move the platform
                return; //Leave this method
            }
            else if (Mathf.Abs(Mathf.Sin(timer)) < .9999f && !m_LoopPosition && !runOnce) //If the platform should run once
            {
                transform.position = startPosition + new Vector3(Mathf.Sin(timer * moveSpeedX) * moveDistanceX, Mathf.Sin(timer * moveSpeedY) * moveDistanceY, 0); //Move the platform
            }
            else //If the platform has run once
            {
                runOnce = true; //Set runOnce to true
            }
        }
    }

    void transformRotation() //Rotates the platform
    {
        transform.RotateAround(rend.bounds.center, Vector3.forward, Time.deltaTime * m_DegreesPerSecond); //Rotate around the Z axis
    }
}