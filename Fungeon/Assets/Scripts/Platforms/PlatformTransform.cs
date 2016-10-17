using UnityEngine;
using System.Collections;

public class PlatformTransform : MonoBehaviour //KI
{
    #region Attributes
    [SerializeField] private bool m_Position = false; //Inspector checkbox for position transforms
    [Tooltip("When unchecked and moving in both the X and Y directions, movement speed will be averaged")] //m_LoopPosition tooltop
    [SerializeField] private bool m_LoopPosition = false; //Inspector checkbox to loop the movement
    [SerializeField] private bool m_PositionIsTrigger = false; //Inspector checkbox to make the object have a trigger
    
    [Tooltip("Distance the object will move in the X direction. A negative value will make the object move left first")] //moveDistanceX tooltip
    [SerializeField] private float moveDistanceX; //Distance to move the platform in the X direction
    [Tooltip("Distance the object will move in the Y direction. A negative value will make the object move down first")] //moveDistanceY tooltip
    [SerializeField] private float moveDistanceY; //Distance to move the platform in the Y direction
    [Tooltip("Movement speed in the X direction")] //moveSpeedX tooltip
    [SerializeField] private float moveSpeedX; //How fast the platform can move
    [Tooltip("Movement speed in the Y direction")] //moveSpeedY tooltip
    [SerializeField] private float moveSpeedY; //How fast the platform can move
    [Space(10)] //Space out the inspector UI elements
    
    [SerializeField] private bool m_Rotation = false; //Inspector checkbox for rotation transforms
    [SerializeField] private bool m_RotationIsTrigger = false; //Inspector checkbox to make the object have a trigger
    [Tooltip("How many degrees that platform will rotate each second. A negative value will make the object rotate right")] //m_RotationDegreesPerSecond tooltip
    [SerializeField] private float m_RotationDPS; //How many degrees to rotate the platform by each second
    [Space(10)] //Space out the inspector UI elements
    
    [SerializeField] private bool m_Circle = false; //Inspector checkbox to move platforms in a circle
    [SerializeField] private bool m_CircleIsTrigger = false; //Inspector checkbox to make the object have a trigger
    [Tooltip("The point at which the platform rotates around")] //m_CenterOfRotation tooltip
    [SerializeField] private Vector3 m_CenterOfRotation; //The point at which the platform rotates around
    [Tooltip("How many degrees that platform will rotate each second. A negative value will make the object rotate right")] //m_CircleDegreesPerSecond tooltip
    [SerializeField] private float m_CircleDPS; //How many degrees to move the circle by each second
    [Space(10)] //Space out the inspector UI elements
    
    [Tooltip("Sticky does not apply to rotation transforms")] //m_Sticy tooltip
    [SerializeField] private bool m_Sticky = false; //Inspector checkbox to make an object sticky
    
    private SpriteRenderer rend; //Renderer for object sizes
    private Vector3 startPosition; //The starting position of the object
    private Vector3 arcRotation; //How far to move the platform each frame
    private bool runOnce = false; //Controls the movement of the platform when it is not looping
    private float timer; //Timer for movement

    public bool M_Position //M_Position property
    {
        get
        {
            return m_Position; //Return the value of m_Position
        }
    }
    public bool M_PositionIsTrigger //M_PositionIsTrigger property
    {
        get
        {
            return m_PositionIsTrigger; //Return the value of m_PositionIsTrigger
        }
        set
        {
            m_PositionIsTrigger = value; //Set m_PositionIsTrigger equal to the value
        }
    }

    public bool M_Rotation //M_Rotation property
    {
        get
        {
            return m_Rotation; //Return the value of m_Rotation
        }
    }
    public bool M_RotationIsTrigger //M_RotationIsTrigger property
    {
        get
        {
            return m_RotationIsTrigger; //Return the value of m_RotationIsTrigger
        }
        set
        {
            m_PositionIsTrigger = value; //Set m_RotationIsTrigger equal to the value
        }
    }

    public bool M_Circle //M_Circle property
    {
        get
        {
            return m_Circle; //Return the value of m_Circle
        }
    }
    public bool M_CircleIsTrigger //M_CircleIsTrigger property
    {
        get
        {
            return m_CircleIsTrigger; //Return the value of m_CircleIsTrigger
        }
        set
        {
            m_CircleIsTrigger = value; //Set m_CircleIsTrigger equal to the value
        }
    }

    public bool M_Sticky //M_Sticky property
    {
        get
        {
            return m_Sticky; //Return the value of m_Sticky
        }
    }

    public float Timer //Timer property
    {
        set
        {
            timer = value; //Set the timer equal to the value
        }
    }
    #endregion

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
        if(m_Circle) //If the toggle box for circling is checked
        {
            arcRotation = transform.position - m_CenterOfRotation; //Get the radius of the circle
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
        if (m_Circle) //If the toggle box for circle transforms is checked
        {
            transformCircle(); //Move the platform in a circle
        }
    
        timer += Time.fixedDeltaTime; //Set the timer
    }
    
    void transformPosition() //Moves the platform
    {
        if (!m_PositionIsTrigger) //Run when not triggered
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
        if (!m_RotationIsTrigger) //Run when not triggered
        {
            transform.RotateAround(rend.bounds.center, Vector3.forward, Time.deltaTime * m_RotationDPS); //Rotate around the Z axis
        }
    }
    
    void transformCircle() //Moves the platform in a circle
    {
        if (!m_CircleIsTrigger) //Run when not triggered
        {
            arcRotation = Quaternion.AngleAxis(m_CircleDPS * Time.deltaTime, Vector3.forward) * arcRotation; //Calculate the arc rotation
            transform.position = m_CenterOfRotation + arcRotation; //Apply the circle's rotation
        }
    }
}