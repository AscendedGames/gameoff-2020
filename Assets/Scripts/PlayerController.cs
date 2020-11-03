using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10; // Floating point variable to store the player's movement speed.
    public float JumpForce = 5; // Floating point variable to store the player's jump force.

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

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
        Jump();
        CheckIfGrounded();
    }

    /// <summary>
    /// This is the method that provides the player the ability to move.
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
    /// This is the method that provides the player the ability to jump.
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpForce);
    }

    /// <summary>
    /// This method checks if the player is on the ground. It is mainly used to determine if they can jump or not.
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
            isGrounded = false;
        }
    }

}