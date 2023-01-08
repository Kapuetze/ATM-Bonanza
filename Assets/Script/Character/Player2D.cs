using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player2D : MonoBehaviour
{
    public float speed = 12f;
    public float jumpHeight = 4f;
    public float airDrag = 2f;
    public AnimationCurve dragCurve;
    [Tooltip("x = minimum multiplier applied to gravity while jumping and pressing jump. y = medium multiplier. z = maximum multiplier while falling.")]
    public Vector3 dynamicGravMultiplier = Vector3.one; 

    public float groundCheckSize = 0.4f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private Vector3 velocity;
    private float grav = -9.81f;
    private float currentDrag = 1f;
    private float airtime = 0f;
    private float x;
    public bool disableControls = false;
    private bool isGrounded = true;
    private bool doubleJumpReady = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        #region ground check
        // lowest point of the collider
        Vector3 low = new Vector3(coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z);

        Collider2D[] targets = Physics2D.OverlapCircleAll(low, groundCheckSize, groundMask);
        // ground check logic
        if (targets.Length > 0)
        {
            BeGrounded();
        }
        else
        {
            Airborn();
        }

        #endregion
        #region Jumping
        if(!disableControls)
        {
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
            else if (Input.GetButtonDown("Jump") && !isGrounded && doubleJumpReady)
            {
                Jump();
                doubleJumpReady = false;
            }
        }
        #endregion

        #region falling
        if(!isGrounded && rb.velocity.y < 0)
        {
            print("falling");
        }
        #endregion

    }

    private void FixedUpdate()
    {
        if(!disableControls) rb.velocity = new Vector2(x * speed * Time.deltaTime, rb.velocity.y);
    }

    public void ForceJump(float tempHeight)
    {
        float temp = jumpHeight;
        jumpHeight = tempHeight;
        BeGrounded();
        Jump();
        jumpHeight = temp;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }

    public void GoRagdoll()
    {
        disableControls = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void StandUp()
    {
        disableControls = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.DORotate(Vector3.zero, 0.5f);
    }

    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * ((grav * rb.gravityScale) * rb.gravityScale));
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * jumpForce;
    }

    private void BeGrounded()
    {
        doubleJumpReady = true;
        isGrounded = true;
        airtime = 0f;
        currentDrag = 1f;

        if (velocity.y < 0)
        {
            velocity.y = -2f;
            rb.gravityScale = dynamicGravMultiplier.y;
        }

        if (x != 0 && !disableControls)
        {
            anim.SetInteger("State", 1);
        }
        if (x == 0 && !disableControls)
        {
            anim.SetInteger("State", 0);
        }
    }

    private void Airborn()
    {
        anim.SetInteger("State", 2);
        isGrounded = false;
        currentDrag = dragCurve.Evaluate(airtime);
        currentDrag = Mathf.Clamp(currentDrag, 1f, airDrag);
        x = x / currentDrag;
        airtime += Time.deltaTime;
    }
}
