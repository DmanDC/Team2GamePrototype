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

    // Reference to Rigidbody2D
    private Rigidbody2D rb;

    // Keep track if we are on the ground
    private bool isGrounded;

    private Animator anim;

    private float horizontalInput;


   

    // Start is called before the first frame update

    private GameObject targetObject;
    void Start()
    {

        targetObject = GameObject.Find("DialogPanel");
        //Get the Rigidbody2D component attached to the game object

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    void Update()
    {
        if (targetObject == true)
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Drive Animator (Idle <-> Walk)
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Face direction
        if (horizontalInput > 0) { 

            transform.rotation = Quaternion.Euler(0, 0, 0);
     
        }

        else if (horizontalInput < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }




}
