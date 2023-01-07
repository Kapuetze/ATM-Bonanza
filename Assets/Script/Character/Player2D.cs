using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public float speed = 12f;
    public float jumpHeight = 4f;
    [Tooltip("x = minimum multiplier applied to gravity while jumping and pressing jump. y = medium multiplier. z = maximum multiplier while falling.")]
    public Vector3 dynamicGravMultiplier = Vector3.one; 

    public float groundCheckSize = 0.4f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector3 velocity;
    public float grav = -9.81f;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region ground check
        // lowest point of the collider
        Vector3 low = new Vector3(coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z);

        Collider2D[] targets = Physics2D.OverlapCircleAll(low, groundCheckSize, groundMask);
        // ground check logic
        if (targets.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            rb.gravityScale = dynamicGravMultiplier.y;
        }

        #endregion

        float x = Input.GetAxis("Horizontal");
        #region Jumping

        if (Input.GetButton("Jump"))
        {
            rb.gravityScale = dynamicGravMultiplier.x;
        }
        else
        {
            rb.gravityScale = dynamicGravMultiplier.z;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        #endregion

        #region falling
        if(!isGrounded && rb.velocity.y < 0)
        {
            print("falling");
        }
        #endregion

        rb.velocity = new Vector2(x * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * ((grav * rb.gravityScale) * rb.gravityScale));
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * jumpForce;
    }
}
