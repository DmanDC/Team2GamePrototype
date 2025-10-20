using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Movement ---
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // --- Ground Check ---
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // --- References ---
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    // --- State ---
    private bool isGrounded;
    private float horizontalInput;

    // --- Optional (for later use) ---
    private bool isShooting = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!!");
        }
    }

    void Update()
    {
        if (isDead) return; // stop control when dead

        // 1) Read movement input (-1, 0, 1)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2) Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 3) Flip player direction
        if (horizontalInput > 0.01f) sr.flipX = false;
        else if (horizontalInput < -0.01f) sr.flipX = true;

        // 4) Shooting (optional, safe placeholder)
        // If you later add shooting logic, toggle "isShooting" true/false there
        // Example: isShooting = Input.GetButton("Fire1");
        anim.SetBool("IsShooting", isShooting);

        // 5) Drive animator (core part)
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // 6) Update grounded state visually if you want jump animations later
        anim.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // Move using physics
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // 7) Safe death trigger (optional)
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        rb.velocity = Vector2.zero;
        anim.ResetTrigger("Die");
        anim.SetTrigger("Die");
    }
}