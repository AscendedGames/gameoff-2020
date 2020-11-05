using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource MouseRun;
    public float stepDelay; //To define the stepDelay inside of Unity

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
    }

    IEnumerator Walking()
    {

        while (couroutineOn == true) {
            if (rigidBody.velocity.magnitude >= 0.2)
            {
                MouseRun.Play();
            }
            else
            {

                MouseRun.Stop();
            }

        yield return new WaitForSeconds(stepDelay);

    }
}
}