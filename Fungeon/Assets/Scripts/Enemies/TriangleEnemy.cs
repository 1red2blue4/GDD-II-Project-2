using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; //List
public class TriangleEnemy : Enemy
{
    //public GameObject target;
    //public float moveSpeed;
    public float attackSpeed;
    
    private FlockManager fm;

    // for detecting when to pursue and attack
    public float pursueRadius;
    public float attackRadius;

    // variables for controlling charge up
    public float chargeDuration = 1.5f;
    private float chargeTimer = 0.0f;
    private bool chargeAttack = false;

    // variables for how long enemies will pursue the player
    public float attackDuration = 3.0f;
    private float attackTimer = 0.0f;
    private bool attackNow = false;

    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;


    //private Rigidbody2D dropsRigidbodies; //The rigidbodies on the drops


    override public void Start()
    {
        //call parent's start
        base.Start();

        fm = GameObject.Find("FlockManagerGO").GetComponent<FlockManager>();

        //for (int i = 0; i < drops.Count; i++) //For each drop in the list of drops
        //{
            //dropsRigidbodies = drops[0].GetComponent<Rigidbody2D>(); //Take the rigidbody from the drop
        //}
    }

    override public void Update() //Physics updates
    {
        // gets the current distance from the enemy to the player
        Vector3 distance = CalcDistance();

        if (distance.magnitude <= attackRadius && attackNow == false)
        {
            chargeAttack = true;
        }
        else if (attackNow == true)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                attackNow = false;
                attackTimer = 0.0f;
            }
            else
            {
                Attack();
            }
        }
        else if (distance.magnitude <= pursueRadius)
        {
            Pursue();
        }
        else
        {
            Idle();
        }

        if(chargeAttack == true)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeDuration)
            {
                chargeAttack = false;
                attackNow = true;
                chargeTimer = 0.0f;
            }
            else
            {
                ChargeAttack();
            }
        }

        //Vector3 offset = player.gameObject.transform.position - this.transform.position;
        //Vector3 unitOffset = offset.normalized;
        //rb.velocity = unitOffset * moveSpeed;
        //rb.velocity = new Vector3(rb.velocity.x, 0, 0);

	}

    /// <summary>
    /// will be called when the bool attack is true
    /// will greatly speed up the enemy
    /// </summary>
    void Attack()
    {
        rb.isKinematic = false;
        Vector3 offset = GameObject.Find("Player").transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        transform.up = unitOffset;
        rb.velocity = unitOffset * attackSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    /// <summary>
    /// function that will "charge" the enemy's attack
    /// should only be called if chargeTimer is less than
    /// chargeDuration
    /// </summary>
    void ChargeAttack()
    {
        rb.isKinematic = true;
        Vector3 offset = GameObject.Find("Player").transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        transform.up = unitOffset;

        // color changing to indicate attack
        float colorTemp = .4f + ((chargeTimer / chargeDuration) * .6f);
        enemySprite.color = new Color(colorTemp, colorTemp, colorTemp);
    }

    /// <summary>
    /// will be called when the bool pursue is true
    /// will have the enemy target the player
    /// </summary>
   void Pursue()
    {
        rb.isKinematic = false;
        Vector3 offset = GameObject.Find("Player").transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        transform.up = unitOffset;
        rb.velocity = unitOffset * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    /// <summary>
    /// true by default, whenever player goes out of range set to true
    /// in the future enemies will wander while in this state
    /// </summary>
    void Idle()
   {
       rb.velocity = new Vector3(0, 0.2f, 0);
   }

    // obstacle avoidance, self explanatory
    //protected Vector3 ObstacleAvoidance(GameObject obstacle) {
    //// set the desired
    //desired = Vector3.zero;
    //
    //// get the distance from the seeker to the obstacle's center
    //Vector3 centerDist = obstacle.transform.position - transform.position;
    //
    //// get the radius of the obstacle
    ////float obstacleRadius = obstacle.GetComponent<ObstacleScript>().Radius;
    ////
    //// //check if objects aren't in the safe zone
    ////if (centerDist.magnitude > safeDistance) {
    ////    return desired;
    ////}
    ////
    ////// if the obstacles are behind the seeker then don't worry about them
    ////if (Vector3.Dot (centerDist, transform.up) < 0) {
    ////    return desired;
    ////}
    ////
    ////// if they're not within the seeker's movement zone then don't worry about them
    ////if (Mathf.Abs (Vector3.Dot (centerDist, transform.right)) > radius + obstacleRadius) {
    ////    return desired;
    ////}
    //
    // //if the code reaches here, there will be a collision!
    //
    // //check whether to steer right or left
    //if (Vector3.Dot (centerDist, transform.right) < 0) {
    //    desired = transform.right * moveSpeed;
    //}
    //
    //if (Vector3.Dot (centerDist, transform.right) >=0) {
    //    desired = transform.right * -moveSpeed;
    //}
    //
    //return desired;
    //}

}