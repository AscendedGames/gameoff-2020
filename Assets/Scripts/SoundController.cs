using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource MouseRun;
    public float stepSoundDelay; //To define the stepDelay inside of Unity

    bool isGrounded = false; // This boolean lets us determine if the player's feet are on the ground or not.
    public Transform isGroundedChecker; // This variable holds the empty GameObject that acts as our ground checker.
    public float checkGroundRadius; // Floating point variable to store the radius of the ground checker.
    public LayerMask groundLayer; // This variable determines the ground layer to enable the groundChecker to let us know we're on the ground

    private Rigidbody2D rigidBody; // Initialize the Rigidbody2D variable to be used later.
    private bool couroutineOn; // For Adding the Coroutine to delay the walking sounds

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        couroutineOn = true;
        StartCoroutine(Walking());
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
    }

    IEnumerator Walking()
    {

        while (couroutineOn == true) {
            if (rigidBody.velocity.magnitude >= 0.2 && isGrounded)
            {
                MouseRun.pitch = (Random.Range(2.2f, 3f));
                MouseRun.volume = (Random.Range(0.358f, 0.580f));
                MouseRun.Play();
            }
            else
            {
                MouseRun.Stop();
            }

            yield return new WaitForSeconds(stepSoundDelay);
        }
    }

    /// <summary>
    /// Checks if the player is on the ground. It is mainly used to determine if they can jump or not.
    /// </summary>
    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null) isGrounded = true;
        else isGrounded = false;
    }
}