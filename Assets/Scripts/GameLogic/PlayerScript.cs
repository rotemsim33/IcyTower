using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ScoreControllerScript scoreManager;
    [SerializeField] private ParticleSystem jumpHighEffect;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private Transform startGround;

    [SerializeField] private float m_moveForce = 300;
    [SerializeField] private float m_jumpForce = 950;
    [SerializeField] private float m_horizontalJumpFactor = 100f;
    [SerializeField] private float m_bounceFactor = 1.2f;
    [SerializeField] private float m_maxJumpForce = 1500;
    [SerializeField] private float m_maxSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb2D;

    private bool isGrounded;
    private bool isFaceRight = true;
    private bool isJump = false;
 
   //Awake is called when the script instance is being loaded.
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    //Update is called every frame.
    private void Update()
    {
        //check if the plyer in on some gound- level or started ground
        isGrounded = Physics2D.Linecast(transform.position, startGround.position, 1 << LayerMask.NameToLayer("Ground"));
        animator.SetBool("isGrounded", isGrounded);

        //if space is hold and the player is on some ground set jump to true
        if (Input.GetButtonDown("Jump") && isGrounded) isJump = true;
    }

    //it is called every fixed frame-rate frame. Compute Physics system calculations after FixedUpdate
    //0.02 seconds (50 calls per second)
    private void FixedUpdate()
    {
        //The value will be -1 for left and 1 for right
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //add force
        if (Mathf.Abs(horizontal * rb2D.velocity.x) < m_maxSpeed) rb2D.AddForce(horizontal * m_moveForce * Vector2.right);

        if (Mathf.Abs(horizontal) <= 0.05) rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        //set the player side
        if ((horizontal > 0 && !isFaceRight) || (horizontal < 0 && isFaceRight)) FlipPlayer();

        //handling jump movment
        if (isJump)
        {
            float totalJumpForce = m_jumpForce + Mathf.Abs(rb2D.velocity.x) * m_horizontalJumpFactor;
            //handling high jump effect
            if (totalJumpForce > m_maxJumpForce)
            {
                jumpHighEffect.Play();
            }
            rb2D.AddForce(Vector2.up * totalJumpForce);
            jumpSound.Play();
            isJump = false;
        }
    }
    private void FlipPlayer()
    {
        isFaceRight = !isFaceRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    //Handling when the player makes contacnt with some incoming collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Level" && isGrounded)
        {
            scoreManager.UpdateScore((int)transform.position.y+3);
        }
        if (collision.gameObject.tag == "Wall")
        {
            FlipPlayer();
            rb2D.AddForce(new Vector2(rb2D.velocity.x * m_bounceFactor, 0), ForceMode2D.Impulse);
        }
      }
}
