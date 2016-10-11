using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool canAttack = true;      // For creating a delay between attacks
        private float timer = 0.0f;         // Timer for the cooldown
        private float coolDown = 1.0f;             // Length the timer has to count up to
        private float defaultGravityScale = 3.0f;

        public GameObject stabPrefab;
        public Camera cam;
        public float slopeFriction;

        //Getter
        public bool CanAttack { get { return canAttack; } }

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            defaultGravityScale = m_Rigidbody2D.gravityScale;
        }

        private void Update()
        {
            if (!canAttack)
            {
                timer += Time.deltaTime;
                if(timer >= coolDown)
                {
                    canAttack = true;
                    timer = 0.0f;
                }
            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    if (colliders[i].gameObject.transform.parent != null)
                    {
                        if (colliders[i].gameObject.transform.parent.tag == "platform")
                        {
                            m_Grounded = true;
                        }
                    }
                    else if(colliders[i].gameObject.tag == "platform")
                    {
                        m_Grounded = true;
                    }
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                Debug.Log("Jump");
                // Add a vertical force to the player.
                m_Rigidbody2D.gravityScale = defaultGravityScale;
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        /// <summary>
        /// Flips the direction the avatar is facing for the player
        /// </summary>
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = this.GetComponentInChildren<SpriteRenderer>().transform.localScale;
            theScale.x *= -1;
            this.GetComponentInChildren<SpriteRenderer>().transform.localScale = theScale;
        }

        /// <summary>
        /// Uses the currently equipped weapon to attack
        /// </summary>
        public void Attack()
        {
            //calculating position to spawn sprite
            Vector3 position = this.transform.position;
            Vector3 scale = stabPrefab.transform.localScale;

            //chekcing which side to attack on based on mouse position
            if (Input.mousePosition.x > (cam.WorldToViewportPoint(this.gameObject.transform.position).x) * Screen.width)
            {
                position.x += this.gameObject.GetComponent<BoxCollider2D>().size.x;
            }
            else
            {
                position.x -= this.gameObject.GetComponent<BoxCollider2D>().size.x;
                scale.x *= -1;
            }
            position.y = this.gameObject.transform.FindChild("Center").transform.position.y;

            //Need to make a weapon class and reimpliment this.
            GameObject weapon = (GameObject)Instantiate(stabPrefab, position, this.transform.rotation);
            weapon.transform.localScale = scale;
            weapon.transform.parent = this.gameObject.transform;
            canAttack = false;
        }

        public void NormalizeSlope(float move)
        {
            if(m_Grounded)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.5f, m_WhatIsGround);
                if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
                {
                    //trying to not slide down slopes

                    //m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x - (hit.normal.x * 0.6f), m_Rigidbody2D.velocity.y);
                    ////m_Rigidbody2D.velocity = new Vector2(-move * m_MaxSpeed * hit.normal.x, move * m_Rigidbody2D.velocity.y * hit.normal.y);
                    //Vector3 pos = transform.position;
                    //pos.y += -hit.normal.x * Mathf.Abs(m_Rigidbody2D.velocity.x) * Time.deltaTime * (m_Rigidbody2D.velocity.x - hit.normal.x > 0 ? 1 : -1);
                    //transform.position = pos;
                }
                
            }
        }
    }
}