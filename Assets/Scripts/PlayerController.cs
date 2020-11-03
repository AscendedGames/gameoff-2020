using UnityEngine;
using System.Collections;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    public float MovementSpeed = 10; // Floating point variable to store the player's movement speed.
    public float JumpForce = 5; // Floating point variable to store the player's jump force.

    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        // Get and store a reference to the Rigidbody2D component so that we can access it.
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {
        Move();

        Jump();
    }

    void Move()
    {
        // Assign the Horizontal movement key to a variable
        var moveHorizontal = Input.GetAxis("Horizontal");

        float movement = moveHorizontal * MovementSpeed;
        rigidBody.velocity = new Vector2(movement, rigidBody.velocity.y);

        // Apply the movement, multiplying by Time.DeltaTime to smooth out movement, then multiply by MovementSpeed.
        //transform.rotation = transform.rotation + new Vector3(moveHorizontal, 0, 0) * Time.deltaTime * MovementSpeed;

        //if (Mathf.Approximately(0, moveHorizontal)) transform.rotation = moveHorizontal > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidBody.velocity.y) < 0.001f)
        {
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
}