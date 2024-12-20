using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerExample : MonoBehaviour
{
    public float speed = 12f;
    [Header("Jump")]
    public float jumpHeight = 4f;
    [Header("Ground Check")]
    public float groundCheckSize = 0.4f;
    public LayerMask groundMask;
    public AudioClip jumpSound;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private AudioSource audioSource;
    private float grav = -9.81f;
    private float x;
    private bool disableControls = false;
    private bool isGrounded = true;
    private bool doubleJumpReady = true;

    public Vector2 gravMultiplyer = Vector2.one;

    public AnimationCurve dragCurve = new AnimationCurve();

    private float airbornTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        gravMultiplyer = new Vector2(rb.gravityScale, gravMultiplyer.y);
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
            Debug.Log("<color=green>Grounded</color>");
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
                rb.gravityScale = gravMultiplyer.y;
            }
            else if (!Input.GetButton("Jump"))
            {
                rb.gravityScale = gravMultiplyer.x;
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("Example", true);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Example", false);
        }

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

    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * ((grav * rb.gravityScale) * rb.gravityScale));
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * jumpForce;
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    private void BeGrounded()
    {
        doubleJumpReady = true;
        isGrounded = true;
        airbornTimer = 0;

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
        float currentDrag = dragCurve.Evaluate(airbornTimer);
        currentDrag = Mathf.Clamp(currentDrag, 1, 10);

        airbornTimer += Time.deltaTime;

        x = x / currentDrag;

        isGrounded = false;
    }
}
