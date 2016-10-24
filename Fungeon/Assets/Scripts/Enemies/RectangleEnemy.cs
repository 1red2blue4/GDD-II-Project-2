using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class RectangleEnemy : Enemy
    {

        //-----------------------VARIABLE DECLARATION------------------------
        // variables to track speed
        public float patrolSpeed = 0.5f;
        public float pursueSpeed = 3.0f;
        public float attackSpeed = 6.0f;

        // bools for tracking states
        private bool playerDetected = false;
        private bool edgeDetected = true;

        // for raycasting
        public float detectPlayerDistance = 10.0f;
        private RaycastHit2D playerCheck;

        //-------------------------------------------------------------------

        // Use this for initialization
        override public void Start()
        {
            base.Start();

        }

        // Update is called once per frame
        override public void Update()
        {

            if (DetectEdge() == true)
            {
                Debug.Log("Edge detected");
            }

            // debug stuff
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x + 0.65f, (transform.position.y - enemySprite.bounds.size.y / 2) - 0.15f ,  0), Color.white);
            Debug.DrawRay(transform.position, transform.right, Color.white);
            Debug.DrawRay(transform.position, -transform.right, Color.white);
        }

        /// <summary>
        /// what the enemy will do if no player is detected
        /// they will move in a direction until they reach the
        /// platform they are on's edge, if they hit it, then
        /// swap their direction
        /// </summary>
        public void Patrol()
        {

        }

        /// <summary>
        /// using a raycast angled down and slightly forward from
        /// their position this method will check if the enemy is
        /// about to fall off of a platform
        /// </summary>
        public bool DetectEdge()
        {
            // calculate the vectors used in raycasting
            Vector3 edgeDetect = new Vector3(transform.position.x + 1.0f, -1 * (transform.position.y - enemySprite.bounds.size.y / 2) - 0.15f, 0);
            Vector3 edgeDetectNorm = edgeDetect.normalized;

            RaycastHit2D edgeCheck = Physics2D.Raycast(new Vector3(transform.position.x + 0.4f, transform.position.y, 0), edgeDetectNorm, 1.0f);

            Debug.DrawRay(new Vector3(transform.position.x + 0.4f, transform.position.y, 0), edgeDetectNorm * 1.0f, Color.white);

            Debug.Log(edgeCheck.collider.transform.parent.tag);

            if (edgeCheck.collider.transform.parent.tag == "platform")
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
        public bool DetectPlayer()
        {
            return false;
        }
    }
}