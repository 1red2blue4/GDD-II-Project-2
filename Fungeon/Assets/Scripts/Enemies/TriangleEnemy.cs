using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; //List

namespace UnityStandardAssets._2D
{
    public class TriangleEnemy : Enemy
    {
        //public GameObject target;
        //public float moveSpeed;


        private Color baseColor;

        private FlockManager fm;

        [SerializeField]
        private GameObject player;


        //private Rigidbody2D dropsRigidbodies; //The rigidbodies on the drops


        override public void Start()
        {
            //call parent's start
            base.Start();

            fm = GameObject.Find("FlockManagerGO").GetComponent<FlockManager>();
            baseColor = this.enemySprite.color;

            //for (int i = 0; i < drops.Count; i++) //For each drop in the list of drops
            //{
            //dropsRigidbodies = drops[0].GetComponent<Rigidbody2D>(); //Take the rigidbody from the drop
            //}
        }

        override public void Update() //Physics updates
        {
            if (!GameManager.Instance.MovingBetweenRooms)
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

                if (chargeAttack == true)
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
        }

        // knockback code
        public void FixedUpdate()
        {
            if (connectTimerStart == true)
            {
                connectTimer += Time.deltaTime;
                if (connectTimer >= connectDuration)
                {
                    connectTimer = 0.0f;
                    knockedback = true;
                    connectTimerStart = false;
                }
            }

            if (knockedback == true)
            {
                knockbackTimer += Time.deltaTime;
                if (knockbackTimer >= knockbackDuration)
                {
                    knockbackTimer = 0.0f;
                    knockedback = false;
                    attackNow = false;
                }
                else
                {
                    Vector2 knockbackDirection = -transform.up;

                    Vector2 knockbackForce = (knockbackDirection * knockback);

                    rb.velocity += knockbackForce;
                }
            }
        }

        /// <summary>
        /// will be called when the bool attack is true
        /// will greatly speed up the enemy
        /// </summary>
        public override void Attack()
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
            float colorTemp = .2f + ((chargeTimer / chargeDuration) * .8f);
            enemySprite.color = GameManager.Instance.LerpColor(baseColor, colorTemp);
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
            // .2f counteracts gravity and causes the enemy to float
            rb.velocity = new Vector3(0, 0.2f, 0);
        }

        public override void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "weapon") //If the collision is with a weapon
            {
                if (dropPercentage[0] >= Random.Range(0, 101)) //If the drop percentage is less than the random number generated
                {
                    Instantiate(drops[0].gameObject, transform.position, Quaternion.identity); //Spawn in a drop
                }

                AudioSource.PlayClipAtPoint(dyingSound.clip, transform.position);
                Destroy(this.gameObject); //Destroy this gameobject
            }

            // apply a knockback force
            if (coll.gameObject.tag == "Player")
            {
                Debug.Log("Hit player");

                connectTimerStart = true;
            }
        }

    }
}