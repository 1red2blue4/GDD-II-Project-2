using UnityEngine;
using System.Collections;
using System.Collections.Generic; //List

public class TriangleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<GameObject> drops; //The drops

    private Rigidbody2D rb;
    //private Rigidbody2D dropsRigidbodies; //The rigidbodies on the drops

	void Start() //Use this for initialization
    {
        rb = this.GetComponent<Rigidbody2D>();

        //for (int i = 0; i < drops.Count; i++) //For each drop in the list of drops
        //{
            //dropsRigidbodies = drops[0].GetComponent<Rigidbody2D>(); //Take the rigidbody from the drop
        //}
    }
	
	void FixedUpdate() //Physics updates
    {
        Vector3 offset = player.gameObject.transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        rb.velocity = unitOffset * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
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
}