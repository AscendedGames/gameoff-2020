using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    PlayerController pController;
    public AudioSource runSound; // Creates the Run Sound Hook into Unity

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pController.isGrounded == true && pController.rigidBody.velocity.magnitude > 2f && runSound == false)
        {
            runSound.Play();
        }

    }
}
