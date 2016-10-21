using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; //List
public class TriangleEnemy : MonoBehaviour
{
    public GameObject target;
    //public float moveSpeed;
    public float attackSpeed;

    private Rigidbody2D rb;
    protected FlockManager fm;

    //movement
    protected Vector3 acceleration; //   CURRENTLY NOT IN USE
    protected Vector3 velocity; //   CURRENTLY NOT IN USE
    protected Vector3 desired; //   CURRENTLY NOT IN USE

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

    // different states for the enemy
    //public enum States
    //{
    //    Idle,
    //    Pursue,
    //    ChargeAttack,
    //    Attack
    //}

    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<GameObject> drops; //The drops

    //private Rigidbody2D dropsRigidbodies; //The rigidbodies on the drops


	void Start() //Use this for initialization
    {
        rb = this.GetComponent<Rigidbody2D>();

        fm = GameObject.Find("FlockManagerGO").GetComponent<FlockManager>();

        // set the current state for the enemy, always start in idle
        //States currentState = Idle;

        //for (int i = 0; i < drops.Count; i++) //For each drop in the list of drops
        //{
            //dropsRigidbodies = drops[0].GetComponent<Rigidbody2D>(); //Take the rigidbody from the drop
        //}
    }

    void Update() //Physics updates
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

    void OnTriggerEnter2D(Collider2D coll) //When a collision occurs
    {

        if (coll.gameObject.tag == "weapon") //If the collision is with a weapon
        {
            Instantiate(drops[0].gameObject, transform.position, Quaternion.identity); //Spawn in a drop
            //dropsRigidbodies.velocity = new Vector2(Random.Range(-10, 10), 1f); //Fling the drop in a random direction

            Destroy(this.gameObject); //Destroy this gameobject
        }
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
    }

    /// <summary>
    /// will be called when the bool pursue is true
    /// will have the enemy target the player
    /// </summary>
   void Pursue()
    {
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
       rb.velocity = new Vector3(0, 0, 0);
   }

    //protected Vector3 Seek(Vector3 targetPos)
    //{
    //    desired = targetPos - transform.position;
    //    desired = desired.normalized * moveSpeed;
    //    desired -= velocity;
    //    desired.z = 0;
    //    return desired;
    //}
    //
    //public Vector3 Cohesion()
    //{
    //    Vector3 centerForce = Seek(gm.barbCentroidObject.transform.position);
    //
    //    return centerForce;
    //}

    /// <summary>
    /// calculates the distance from the enemy to the player
    /// </summary>
    Vector3 CalcDistance()
    {
        return GameObject.Find("Player").transform.position - this.transform.position;
    }
}