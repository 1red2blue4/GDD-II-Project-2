using UnityEngine;
using System.Collections;

public class RectangleEnemy : Enemy {

    //-----------------------VARIABLE DECLARATION------------------------
    // variables to track speed
    public float patrolSpeed;
    public float pursueSpeed;
    public float attackSpeed;

    // bools for tracking states
    private bool playerDetected = false;
    private bool edgeDetected = true;
    private int direction = 1;

    // for raycasting
    public float detectPlayerDistance = 10.0f;
    private RaycastHit2D playerCheck;

    //-------------------------------------------------------------------

	// Use this for initialization
	override public void Start () {
        base.Start();

	}
	
	// Update is called once per frame
	override public void Update () {

        Patrol();

        //if (DetectRightEdge() == true)
        //{
        //    Debug.Log("Right Edge detected");
        //}
        //if (DetectLeftEdge() == true)
        //{
        //    Debug.Log("Left Edge detected");
        //}
        //if (DetectLeftPlayer() == true)
        //{
        //    Debug.Log("Player Left");
        //}
        //if (DetectRightPlayer() == true)
        //{
        //    Debug.Log("Player Right");
        //}

	}

    /// <summary>
    /// what the enemy will do if no player is detected
    /// they will move in a direction until they reach the
    /// platform they are on's edge, if they hit it, then
    /// swap their direction
    /// </summary>
    public void Patrol()
    {
        bool left = DetectLeftEdge();
        bool right = DetectRightEdge();

        Debug.Log("Right: " + right);
        Debug.Log("Left: " + left);

        // make series of if statements to check where to partol
        // patrol right
        if (direction == 1 && right == true)
        {
            Debug.Log("Going Right...");
            rb.velocity = new Vector3(patrolSpeed, 0);
        }

        // turn left
        if (direction == 1 && right == false)
        {
            Flip();
            Debug.Log("Turning Left...");
            direction = -1;
        }

        // patrol left
        if (direction == -1 && left == true)
        {
            Debug.Log("Going Left...");
            rb.velocity = new Vector3(-patrolSpeed, 0);
        }

        // turn right
        if (direction == -1 && left == false)
        {
            Flip();
            Debug.Log("Turning Right...");
            direction = 1;
        }
    }

    /// <summary>
    /// flips the enemy
    /// </summary>
    public void Flip()
    {
        // flip enemy
        Vector3 theScale = this.GetComponentInChildren<SpriteRenderer>().transform.localScale;
        theScale.x *= -1;
        this.GetComponentInChildren<SpriteRenderer>().transform.localScale = theScale;
    }

    /// <summary>
    /// using a raycast angled down and slightly forward from
    /// their position this method will check if the enemy is
    /// about to fall off of a platform
    /// </summary>
    public bool DetectRightEdge()
    {
        // calculate the vectors used in raycasting
        Vector3 edgeDetect = new Vector3(transform.position.x + 1.0f, -1 * (transform.position.y - enemySprite.bounds.size.y / 2) - 0.15f, 0);
        Vector3 edgeDetectNorm = edgeDetect.normalized;

        RaycastHit2D edgeCheck = Physics2D.Raycast(new Vector3(transform.position.x + 0.4f, transform.position.y, 0), edgeDetectNorm, 1.0f);

        Debug.DrawRay(new Vector3(transform.position.x + 0.4f, transform.position.y, 0), edgeDetectNorm * 1.0f, Color.white);

        //Debug.Log(edgeCheck.collider.transform.parent.tag);

        //if (edgeCheck.collider.transform.parent.tag == "platform")
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        if (edgeCheck.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// using a raycast angled down and slightly forward from
    /// their position this method will check if the enemy is
    /// about to fall off of a platform
    /// </summary>
    public bool DetectLeftEdge()
    {
        // calculate the vectors used in raycasting
        Vector3 edgeDetect = new Vector3(transform.position.x - 1.0f, (transform.position.y - enemySprite.bounds.size.y / 2) - 0.15f, 0);
        Vector3 edgeDetectNorm = edgeDetect.normalized * -1;

        RaycastHit2D edgeCheck = Physics2D.Raycast(new Vector3(transform.position.x - 0.4f, transform.position.y, 0), edgeDetectNorm, 1.0f);

        Debug.DrawRay(new Vector3(transform.position.x - 0.4f, transform.position.y, 0), edgeDetectNorm * 1.0f, Color.white);

        //Debug.Log(edgeCheck.collider.transform.parent.tag);

        //if (edgeCheck.collider.transform.parent.tag == "platform")
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        if (edgeCheck.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// using 2 raycasts that shoot off from the left and right of
    /// the enemy this enemy will check if it can "see" a player
    /// if so, it will enter pursue mode
    /// </summary>
    public bool DetectRightPlayer()
    {
        // calculate vectors used in raycasting
        Vector3 playerDetectRight = transform.right;
        Vector3 playerDetectRightNorm = playerDetectRight.normalized;

        RaycastHit2D playeeRightCheck = Physics2D.Raycast(new Vector3((transform.position.x + (enemySprite.bounds.size.x / 2) + 0.1f), transform.position.y, 0), playerDetectRightNorm, Mathf.Infinity);

        Debug.DrawRay(new Vector3((transform.position.x + (enemySprite.bounds.size.x / 2) + 0.1f), transform.position.y, 0), playerDetectRightNorm * Mathf.Infinity, Color.white);


        if(playeeRightCheck.transform.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// using 2 raycasts that shoot off from the left and right of
    /// the enemy this enemy will check if it can "see" a player
    /// if so, it will enter pursue mode
    /// </summary>
    public bool DetectLeftPlayer()
    {
        // calculate vectors used in raycasting
        Vector3 playerDetectLeft = -transform.right;
        Vector3 playerDetectLeftNorm = playerDetectLeft.normalized;

        RaycastHit2D playerLeftCheck = Physics2D.Raycast(new Vector3((transform.position.x - (enemySprite.bounds.size.x / 2) - 0.1f), transform.position.y, 0), playerDetectLeftNorm, Mathf.Infinity);

        Debug.DrawRay(new Vector3((transform.position.x - (enemySprite.bounds.size.x / 2) - 0.1f), transform.position.y, 0), playerDetectLeftNorm * Mathf.Infinity, Color.white);

        if (playerLeftCheck.transform.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
