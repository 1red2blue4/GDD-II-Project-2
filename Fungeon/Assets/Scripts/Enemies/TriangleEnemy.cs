using UnityEngine;
using System.Collections;

public class TriangleEnemy : MonoBehaviour {

    public GameObject player;
    public float moveSpeed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = player.gameObject.transform.position -  this.transform.position;
        Vector3 unitOffset = offset.normalized;
        rb.velocity = unitOffset * moveSpeed;
	}
}
