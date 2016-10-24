using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets._2D
{
    /// <summary>
    /// This is the base enemy class from which all enemies will inherit from
    /// </summary>
    abstract public class Enemy : MonoBehaviour
    {

        public int damage;
        public int health;
        public float moveSpeed;
        public float defaultMoveSpeed;
        public float attackSpeed;
        // for detecting when to pursue and attack
        public float attackRadius;

        // variables for controlling charge up
        public float chargeDuration = 1.5f;
        protected float chargeTimer = 0.0f;
        protected bool chargeAttack = false;

        // variables for how long enemies will pursue the player
        public float attackDuration = 3.0f;
        protected float attackTimer = 0.0f;
        protected bool attackNow = false;

        protected Rigidbody2D rb;

        protected SpriteRenderer enemySprite;

        protected AudioSource dyingSound;
        //protected AudioClip death;

        [SerializeField]
        private List<GameObject> drops; //The drops

        public SpriteRenderer EnemySprite { get { return enemySprite; } }

        // Use this for initialization
        virtual public void Start()
        {

            dyingSound = this.GetComponent<AudioSource>();
            //death = this.GetComponent<AudioSource>().GetComponent<AudioClip>();

            rb = this.GetComponent<Rigidbody2D>();

            enemySprite = GetComponentInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        abstract public void Update();

        /// <summary>
        /// Handles what happens when an enemy collides with a weapon
        /// </summary>
        /// <param name="coll"></param>
        public void OnTriggerEnter2D(Collider2D coll) //When a collision occurs
    {
        if (coll.gameObject.tag == "weapon") //If the collision is with a weapon
        {
            health -= GameManager.Instance.Player.damage;
            if(health <= 0)
            {
                //Spawn in a drop
                Instantiate(drops[0].gameObject, transform.position, Quaternion.identity);
                //dropsRigidbodies.velocity = new Vector2(Random.Range(-10, 10), 1f); //Fling the drop in a random direction

                AudioSource.PlayClipAtPoint(dyingSound.clip, transform.position);

                Destroy(this.gameObject); //Destroy this gameobject
            }
        }
    }

        /// <summary>
        /// calculates the distance from the enemy to the player
        /// </summary>
        public Vector3 CalcDistance()
        {
            return GameObject.Find("Player").transform.position - this.transform.position;
        }
    }
}