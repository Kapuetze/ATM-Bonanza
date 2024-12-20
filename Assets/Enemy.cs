using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class EnemyList
{
    private static EnemyList INSTANCE;
    public List<Enemy> _enemyList;

    public static EnemyList GetList()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new EnemyList();
        }

        return INSTANCE;
    }
}


public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public ContactFilter2D contactFilter;
    public float radius = 0.3f;
    public float playerSeekRadius = 2.0f;
    private float facingDirection = -1;
    private Collider2D collider2D;
    public LayerMask layerMask;
    private LineRenderer lineRenderer;
    public Vector2 speed = Vector2.up;
    private float actualSpeed = 1;
    private bool jumpReady = true;
    private bool isGrounded = true;
    public float groundCheckSize = 0.3f;
    public LayerMask groundMask;
    private float jumpHeight = 6;
    private float grav = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        EnemyList.GetList()._enemyList.Add(this);

        var textFile = Resources.Load<TextAsset>("Text/textFile01");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 low = new Vector3(collider2D.bounds.center.x, collider2D.bounds.min.y, collider2D.bounds.center.z);

        Collider2D[] targets = Physics2D.OverlapCircleAll(low, groundCheckSize, groundMask);
        // ground check logic
        if (targets.Length > 0)
        {
            isGrounded = true;
            jumpReady = true;
        }
        else
        {
            isGrounded = false;
        }

        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        if (0 < Physics2D.CircleCast(transform.position, radius, rb.velocity, contactFilter, hit, 1))
        {
            facingDirection *= -1;
        }

        Collider2D[] playerHit = Physics2D.OverlapCircleAll(transform.position, playerSeekRadius, layerMask);
        if (0 < playerHit.Length)
        {
            Hunt(playerHit[0]);
        } else
        {
            lineRenderer.positionCount = 0;
            actualSpeed = speed.x;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(facingDirection * actualSpeed * Time.deltaTime, rb.velocity.y, 0);
    }

    private void Hunt(Collider2D collider2D)
    {
        Vector3 playerPos = collider2D.gameObject.transform.position;
        Vector3 dir = playerPos - transform.position;
        Debug.Log("<color=red>EnemyVector: </color>" + dir.normalized);
        if (dir.normalized.x < 0)
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, playerPos);
        lineRenderer.SetPosition(1, transform.position);
        actualSpeed = speed.y;

        if (dir.normalized.y > 0.5f && isGrounded && jumpReady)
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * ((grav * rb.gravityScale) * rb.gravityScale));
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * jumpForce;
        jumpReady = false;
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint2D c in collision.contacts)
            {
                float distance = Vector2.Distance(c.point, new Vector2(collider2D.bounds.center.x, collider2D.bounds.max.y));
                Debug.Log(distance);
                if (distance <= 0.29)
                {
                    Debug.Log("Hit");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Ensure we have a Rigidbody2D attached
        if (rb == null)
            return;

        // Parameters for the CircleCast
        Vector2 castOrigin = transform.position;
        Vector2 direction = rb.velocity.normalized;  // Direction of velocity
        float distance = 1f;  // You can adjust this distance as needed

        // Color for the gizmos
        Gizmos.color = Color.red;

        // Draw the CircleCast's radius as a sphere at the starting point
        Gizmos.DrawWireSphere(castOrigin + direction * distance, radius);

        // Draw the direction of the cast as a line
        Gizmos.color = Color.green;
        Gizmos.DrawLine(castOrigin, castOrigin + direction * distance);

        // Perform the CircleCast to detect any collisions (optional)
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, radius, direction, distance, contactFilter.layerMask);

        // If there's a hit, draw a marker at the hit point
        if (hit.collider != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hit.point, 0.1f);  // Draw a small sphere at the hit point
        }

        if (collider2D == null)
            collider2D = GetComponent<Collider2D>();

        // Draw the collider bounds (this helps visualize the bounds of the object)
        Gizmos.color = Color.yellow;

        if (collider2D != null)
        {   // Example contact points to visualize, in real code you'd compute based on actual hits
            Vector2 topEdge = new Vector2(collider2D.bounds.center.x, collider2D.bounds.max.y);
            Gizmos.DrawSphere(topEdge, 0.1f);  // Adjust this to the actual contact point you want to visualize
        }

        // Gizmos-Farbe setzen (optional, für Sichtbarkeit)
        Gizmos.color = Color.green;

        // Gizmo für den Kreis zeichnen, der den Spieler-Radius anzeigt
        Gizmos.DrawWireSphere(transform.position, playerSeekRadius);

    }
}
