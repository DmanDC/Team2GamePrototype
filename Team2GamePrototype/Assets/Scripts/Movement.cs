/* Author: Adam Krenek
 * Date: 9/22/2025
 * Description: Controls platformer player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Player movement speed
    public float moveSpeed = 5f;

    // Force applied when jumping
    public float jumpForce = 10f;

    // Layer mask for detecting ground
    public LayerMask groundLayer;

    // Transform for the position to check for ground
    public Transform groundCheck;

    // Radius for ground check
    public float groundCheckRadius = 0.2f;

    // References
    private Rigidbody2D rb;
    private Animator anim;

    // State
    private bool isGrounded;
    private float horizontalInput;

    // Optional: block movement when a dialog panel is visible
    private GameObject dialogPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Optional: only used if a UI panel named "DialogPanel" exists
        dialogPanel = GameObject.Find("DialogPanel");

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    void Update()
    {
        // If a dialog panel exists and is visible, freeze movement
        if (dialogPanel != null && dialogPanel.activeInHierarchy)
        {
            horizontalInput = 0f;
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Move
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Ground check
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // Drive Animator (Idle <-> Walk)
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        // Face direction
        if (horizontalInput > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (horizontalInput < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}