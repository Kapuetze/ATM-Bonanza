using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private float xInput = 0;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 25;
    public float jumpVelocity = 1;
    public ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Rigidbody2D>(out rb))
        {
            Debug.LogError("kein rigid body");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
    }

    private bool isGrounded()
    {
        Collider2D[] result = new Collider2D[10];
        int i = Physics2D.OverlapCircle(transform.position + Vector3.down, 1, contactFilter, result);
        if (i > 0)
        {
            return true;
        }
        return false;
    }

    private void Jump()
    {
        Vector2 v = new Vector2(rb.velocity.x, jumpVelocity);
        rb.velocity = v;
    }

    private void FixedUpdate()
    {
        Vector2 v = new Vector2(xInput * Time.deltaTime * speed, rb.velocity.y);
        rb.velocity = v;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + Vector3.down, 1);
    }
}
