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


    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<GameObject> drops; //The drops

    //private Rigidbody2D dropsRigidbodies; //The rigidbodies on the drops


	void Start() //Use this for initialization
    {
        rb = this.GetComponent<Rigidbody2D>();

        fm = GameObject.Find("FlockManagerGO").GetComponent<FlockManager>();

        //for (int i = 0; i < drops.Count; i++) //For each drop in the list of drops
        //{
            //dropsRigidbodies = drops[0].GetComponent<Rigidbody2D>(); //Take the rigidbody from the drop
        //}
    }

    void Update() //Physics updates
    {
        Vector3 distance = CalcDistance();

        if (distance.magnitude <= attackRadius)
        {
            Attack();
        }
        else if (distance.magnitude <= pursueRadius)
        {
            Pursue();
        }
        else
        {
            Idle();
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

        /*
        if (coll.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            RoomManager manager = new RoomManager();
            manager.SpawnRooms();
        }
        */
    }

    /// <summary>
    /// will be called when the bool attack is true
    /// will greatly speed up the enemy
    /// </summary>
    void Attack()
    {
        Vector3 offset = GameObject.Find("Player").transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        transform.up = unitOffset;
        rb.velocity = unitOffset * attackSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
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
    //Vector3 Seperation()
    //{
    //    // this is the total flee force that will be the sum of any number of flockers fleeing
    //    Vector3 totalFleeForce = new Vector3(0, 0, 0);
    //
    //    // loop through all vehicles and see if any are close to this one
    //    for (int i = 0; i < fm.EnemyFlock.Count; i++)
    //    {
    //        // calculate the distance between this flocker and another
    //        Vector3 distance = fm.EnemyFlock[i].transform.position - transform.position;
    //
    //        // check if this distance is greater than zero (we don't want the flocker fleeing from itself!)
    //        // and if it less than the seperation distance then this flocker will flee this flocker
    //        if (distance.magnitude > 0 && distance.magnitude < sepRadius)
    //        {
    //            Vector3 partialFleeForce = Flee(fm.EnemyFlock[i].transform.position);
    //
    //            // add the partial flee force to the total one
    //            totalFleeForce = partialFleeForce + totalFleeForce;
    //        }
    //    }
    //    // this should be the combined seperation force
    //    return totalFleeForce;
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