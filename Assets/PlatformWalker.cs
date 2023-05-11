using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWalker : MonoBehaviour
{
    public bool facingRight;
    protected bool jump;
    public float maxSpeed = 5.0f;
    public float maxForce = 365.0f;
    public float horizInput = 1.0f;
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Animator animationController;
    protected Rigidbody2D rb;
    protected BoxCollider2D floorTrigger;
    protected CapsuleCollider2D characterCollider;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        floorTrigger = GetComponent<BoxCollider2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        if (facingRight == false)
        {
            Flip();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Mathf.Abs(horizInput *(rb.velocity.x)) < maxSpeed)
        {
            rb.AddForce(horizInput * Vector2.right * maxForce);
        }
        if (Mathf.Abs(rb.velocity.x) >maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x)* maxSpeed, rb.velocity.y);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Flip();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag =="Player")
        {
            if(other.rigidbody.velocity.y<-0.1f)
            {
                horizInput = 0;
                animator.SetTrigger("Death");


            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        horizInput *= -1;

        transform.localScale  = new Vector3((transform.localScale.x* -1), transform.localScale.y, transform.localScale.z);
        
    }
    void Death2()
    {
        Destroy(gameObject);
    }
}
