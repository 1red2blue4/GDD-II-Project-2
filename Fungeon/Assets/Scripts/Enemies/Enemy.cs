using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the base enemy class from which all enemies will inherit from
/// </summary>
abstract public class Enemy : MonoBehaviour {

    // for detecting when to pursue and attack
    public float attackRadius;
    public float pursueRadius;

    // variables for controlling charge up
    public float chargeDuration = 1.5f;
    protected float chargeTimer = 0.0f;
    protected bool chargeAttack = false;

    // variables for how long enemies will pursue the player
    public float attackDuration = 3.0f;
    protected float attackTimer = 0.0f;
    protected bool attackNow = false;

    // force for knockback
    public float knockback;
    protected bool knockedback;
    protected float knockbackTimer = 0.0f;
    public float knockbackDuration = 0.5f;

    // for knockback, if enemies are knocked back immediately they don't do damage
    // so, they stay connected for a short time to do damage then are knocked back
    protected float connectTimer = 0.0f;
    protected float connectDuration = 0.1f;
    protected bool connectTimerStart = false;

    protected Rigidbody2D rb;

    protected SpriteRenderer enemySprite;

    protected AudioSource dyingSound;
    //protected AudioClip death;

    [SerializeField]protected List<GameObject> drops; //The drops

    public SpriteRenderer EnemySprite { get { return enemySprite; } }

	// Use this for initialization
	virtual public void Start () {

        dyingSound = this.GetComponent<AudioSource>();
        //death = this.GetComponent<AudioSource>().GetComponent<AudioClip>();

        rb = this.GetComponent<Rigidbody2D>();

        enemySprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
    abstract public void Update();

    /// <summary>
    /// will be called when the bool attack is true
    /// will greatly speed up the enemy
    /// </summary>
    abstract public void Attack();

    /// <summary>
    /// Handles what happens when an enemy collides with a weapon
    /// </summary>
    /// <param name="coll"></param>
    abstract public void OnTriggerEnter2D(Collider2D coll); //When a collision occurs

    /// <summary>
    /// calculates the distance from the enemy to the player
    /// </summary>
    public Vector3 CalcDistance()
    {
        return GameObject.Find("Player").transform.position - this.transform.position;
    }
}
