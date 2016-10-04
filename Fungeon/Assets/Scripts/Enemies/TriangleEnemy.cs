using UnityEngine;
using System.Collections;

public class TriangleEnemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;

    private Rigidbody2D rb;

	void Start() //Use this for initialization
    {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() //Physics updates
    {
        Vector3 offset = player.gameObject.transform.position -  this.transform.position;
        Vector3 unitOffset = offset.normalized;
        rb.velocity = unitOffset * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
	}

    void OnTriggerEnter2D(Collider2D coll) //When a collision occurs
    {
        if (coll.gameObject.tag == "weapon") //If the collision is with a weapon
        {
            Destroy(this.gameObject); //Destroy this gameobject
        }
    }
}