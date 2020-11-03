using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float MovementSpeed = 10; // Floating point variable to store the player's movement speed.
    public float JumpForce = 5; // Floating point variable to store the player's jump force.
    public float fallMultiplier = 2.5f; // Gravity multiplier to help our jumping feel more polished.
    public float lowJumpMultiplier = 2f; // Allows the player to make smaller jumps by holding the Jump key for less time.

    bool isGrounded = false; // This boolean lets us determine if the player's feet are on the ground or not.
    public Transform isGroundedChecker; // This variable holds the empty GameObject that acts as our ground checker.
    public float checkGroundRadius; // Floating point variable to store the radius of the ground checker.
    public LayerMask groundLayer; // This variable determines the ground layer to enable the groundChecker to let us know we're on the ground
    public float rememberGroundedFor; // Helps keep player grounded to allow jumps slightly after the player has run off of a ledge
    float lastTimeGrounded; // Floating point variable to remember the last time the player was grounded

    private Rigidbody2D rigidBody; // Initialize the Rigidbody2D variable to be used later.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Get and store a reference to the Rigidbody2D component so that we can access it.
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called every frame. Update is the most commonly used function to implement any kind of game script.
    /// </summary>
    void Update()
    {
        Move();
        CheckIfGrounded();
        Jump();
        JumpSmoothing();

        // =============== NOTE: REMOVE THIS WHEN WE GET REAL ANIMATIONS ===============
        if (Input.GetAxis("Horizontal") < 0) spriteRenderer.flipX = true;
        else if (Input.GetAxis("Horizontal") > 0) spriteRenderer.flipX = false;
    }

    /// <summary>
    /// Provides the player the ability to move.
    /// </summary>
    void Move()
    {
        // Assign the Horizontal movement key to a variable
        var moveHorizontal = Input.GetAxis("Horizontal");

        // Calculate and apply movement speed.
        float movement = moveHorizontal * MovementSpeed;
        rigidBody.velocity = new Vector2(movement, rigidBody.velocity.y);
    }

    /// <summary>
    /// Provides the player the ability to jump.
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpForce);
        }
    }

    /// <summary>
    /// Checks if the player is on the ground. It is mainly used to determine if they can jump or not.
    /// </summary>
    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    /// <summary>
    /// Helps to polish the jumping physics a bit. Adds functionality
    /// </summary>
    void JumpSmoothing()
    {
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}