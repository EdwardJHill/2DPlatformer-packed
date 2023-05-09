using UnityEngine;
using System.Collections;

//--------------------------------------------
/*Basic Character Controller Includes:  
    - Basic Jumping
    - Basic grounding with line traces
    - Basic horizontal movement
 */
//--------------------------------------------

public class BasicCharacterController : MonoBehaviour
{
    protected bool facingRight = true;
    protected bool jumped;

    public float speed = 5.0f;
    public float jumpForce = 1000;

    private float horizInput;

    public Transform groundedCheckStart;
    public Transform groundedCheckEnd;
    public bool grounded;

    public Rigidbody2D rb;
    public Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        grounded = Physics2D.Linecast(groundedCheckStart.position, groundedCheckEnd.position, 1 << LayerMask.NameToLayer("Level"));
        horizInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            jumped = true;
            Debug.Log("Should jump");
            animator.SetTrigger("Ascending");
        }

        if (jumped == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            Debug.Log("Jumping!");

            jumped = false;
        }

        // Detect if character sprite needs flipping
        if (horizInput > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (horizInput < 0 && facingRight)
        {
            FlipSprite();
        }
        if (  horizInput == 0 && grounded== true    )
        {
            //rb.velocity.x
            animator.SetTrigger("Idle");
        }
        else if ( horizInput != 0 && grounded == true)
        {
            animator.SetTrigger("Run");
        }
        else if ( rb.velocity.y > 0 && grounded == false)
        {
            animator.SetTrigger("Ascending");
        }
        else if (rb.velocity.y < 0 && grounded == false)
        {
            animator.SetTrigger("Falling");
        }
    }
    void FixedUpdate()
    {
        //Linecast to our groundcheck gameobject if we hit a layer called "Level" then we're grounded
        
        Debug.DrawLine(groundedCheckStart.position, groundedCheckEnd.position, Color.red);

        //Get Player input 
        
        //Move Character
        rb.velocity = new Vector2(horizInput * speed * Time.fixedDeltaTime, rb.velocity.y);
        
    }

    // Flip Character Sprite
    void FlipSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
