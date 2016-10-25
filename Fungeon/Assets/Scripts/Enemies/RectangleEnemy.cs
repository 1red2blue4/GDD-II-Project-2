using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class RectangleEnemy : Enemy
    {

        //-----------------------VARIABLE DECLARATION------------------------
        // variables to track speed
        public float patrolSpeed;
        public float pursueSpeed;

        // bools for tracking states
        private bool playerDetected = false;
        private bool edgeDetected = true;
        private bool isPatrolling = true;
        public int direction = -1;

        // for raycasting
        public float detectPlayerDistance;

        //-------------------------------------------------------------------



        // Use this for initialization
        override public void Start()
        {
            base.Start();

            if (direction == -1)
            {
                Vector3 theScale = this.GetComponentInChildren<SpriteRenderer>().transform.localScale;
                theScale.x *= -1;
                this.GetComponentInChildren<SpriteRenderer>().transform.localScale = theScale;
            }
            this.enabled = false;
        }

        override public void Update()
        {
            if (!GameManager.Instance.MovingBetweenRooms)
            {
                if (isPatrolling == true)
                {
                    Patrol();
                }

                // enemy sees player to the right
                if (direction == 1 && isPatrolling == true)
                {
                    if (DetectRightPlayer() == true)
                    {
                        playerDetected = true;
                        isPatrolling = false;
                    }
                }

                // enemy sees player to the left
                if (direction == -1 && isPatrolling == true)
                {
                    if (DetectLeftPlayer() == true)
                    {
                        playerDetected = true;
                        isPatrolling = false;
                    }
                }

                // enemy sees player sneaking up behind it
                //if (DetectRadiusPlayer() == true && isPatrolling == true)
                //{
                //    playerDetected = true;
                //    isPatrolling = false;
                //}


            }
        }

        // handles attacking
        void FixedUpdate()
        {
            if (!GameManager.Instance.MovingBetweenRooms)
            {
                if (playerDetected == true && (DetectLeftEdge() == true || DetectRightEdge() == true))
                {
                    Attack();
                }
            }
            
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

            // make series of if statements to check where to partol
            // patrol right
            if (direction == 1 && right == true)
            {
                //Debug.Log(transform.name + " Going Right at " + rb.velocity.x);
                rb.velocity = new Vector3(patrolSpeed, 0, 0);
                rb.transform.position = new Vector3(transform.position.x, transform.position.y + .001f, 0);
            }

            // turn left
            if (direction == 1 && right == false)
            {
                Flip();
                //Debug.Log("Turning Left...");
            }

            // patrol left
            if (direction == -1 && left == true)
            {
                //Debug.Log(transform.name + " Going Left at" + rb.velocity.x);
                rb.velocity = new Vector3(-patrolSpeed, 0, 0);
                rb.transform.position = new Vector3(transform.position.x, transform.position.y + .001f, 0);
            }

            // turn right
            if (direction == -1 && left == false)
            {
                Flip();
                //Debug.Log("Turning Right...");
            }
        }

        /// <summary>
        /// will be called when the bool attack is true
        /// will greatly speed up the enemy
        /// </summary>
        public override void Attack()
        {
            Vector3 dist = CalcDistance();
            Vector3 offset = CalcDistance();
            Vector3 unitOffset = offset.normalized;
            rb.velocity = new Vector2(unitOffset.x * attackSpeed, 0);

            if (direction == -1 && rb.velocity.x > 0)
            {
                Flip();
            }
            if (direction == 1 && rb.velocity.x < 0)
            {
                Flip();
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
            direction = direction * -1;
        }

        /// <summary>
        /// using a raycast angled down and slightly forward from
        /// their position this method will check if the enemy is
        /// about to fall off of a platform
        /// </summary>
        public bool DetectRightEdge()
        {
            // calculate the vectors used in raycasting
            Vector3 edgeDetect = new Vector3(transform.position.x + 1.0f, (transform.position.y - enemySprite.bounds.size.y / 2) - 0.15f, 0);
            Vector3 originDetect = new Vector3(transform.position.x + 0.4f, transform.position.y, 0);
            Vector3 edgeDetectNorm = (edgeDetect - originDetect).normalized;

            RaycastHit2D edgeCheck = Physics2D.Raycast(originDetect, edgeDetectNorm, 1.5f);

            Debug.DrawRay(new Vector3(transform.position.x + 0.4f, transform.position.y, 0), edgeDetectNorm * 1.5f, Color.red);

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
            Vector3 originDetect = new Vector3(transform.position.x - 0.4f, transform.position.y, 0);
            Vector3 edgeDetectNorm = (edgeDetect - originDetect).normalized;

            RaycastHit2D edgeCheck = Physics2D.Raycast(originDetect, edgeDetectNorm, 1.5f);

            Debug.DrawRay(new Vector3(transform.position.x - 0.4f, transform.position.y, 0), edgeDetectNorm * 1.5f, Color.red);

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

            RaycastHit2D playerRightCheck = Physics2D.Raycast(new Vector3((transform.position.x + (enemySprite.bounds.size.x / 2) + 0.1f), transform.position.y, 0), playerDetectRightNorm, detectPlayerDistance);

            Debug.DrawRay(new Vector3((transform.position.x + (enemySprite.bounds.size.x / 2) + 0.1f), transform.position.y, 0), playerDetectRightNorm * detectPlayerDistance, Color.white);

            try
            {
                if (playerRightCheck.transform.tag == "Player")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
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

            RaycastHit2D playerLeftCheck = Physics2D.Raycast(new Vector3((transform.position.x - (enemySprite.bounds.size.x / 2) - 0.1f), transform.position.y, 0), playerDetectLeftNorm, detectPlayerDistance);

            Debug.DrawRay(new Vector3((transform.position.x - (enemySprite.bounds.size.x / 2) - 0.1f), transform.position.y, 0), playerDetectLeftNorm * detectPlayerDistance, Color.white);

            try
            {
                if (playerLeftCheck.transform.tag == "Player")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// will check if the player is closely behind or in front of
        /// the enemy
        /// </summary>
        /// <returns>if a player is detected</returns>
        public bool DetectRadiusPlayer()
        {
            // gets the current distance from the enemy to the player
            Vector3 distance = CalcDistance();

            if (distance.magnitude <= pursueRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            if (coll.gameObject.transform.parent.tag == "platform")
            {
                direction = direction * -1;
            }

            // apply a knockback force
            //if (coll.gameObject.tag == "Player")
            //{
            //    Debug.Log("Hit player");
            //}
        }
    }
}
