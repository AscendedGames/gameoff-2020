using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSoundControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource walkingSounds;
    [SerializeField]
    private float stepSoundDelay; //To define the stepDelay inside of Unity
    [SerializeField]
    private Animator NPCAnimation;

    private bool couroutineOn; // For Adding the Coroutine to delay the walking sounds

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NPCPursuitController>();
        couroutineOn = true;
        StartCoroutine(Patroling());
    }

    IEnumerator Patroling()
    {

        while (couroutineOn == true)
        {
            if (GetComponent<NPCPursuitController>().IsInPursuit == false && GetComponent<NPCPursuitController>().HasBrokenPursuit == false)
            {
                Debug.Log("Patroling!");
                walkingSounds.enabled = true;
                NPCAnimation.GetComponent<Animator>().Play("Scientist Walk");
                walkingSounds.pitch = (Random.Range(1f, 1.1f));
                walkingSounds.Play();
            }
            else if (GetComponent<NPCPursuitController>().IsInPursuit == true)
            {
                Debug.Log("In Pursuit!");
                walkingSounds.enabled = true;
                NPCAnimation.GetComponent<Animator>().Play("Scientist Walk");
                walkingSounds.pitch = (Random.Range(1.3f, 1.5f));
                walkingSounds.Play();
            }
            else if (GetComponent<NPCPursuitController>().HasBrokenPursuit == true)
            {
                Debug.Log("This Works?");
                NPCAnimation.GetComponent<Animator>().Play("Scientist-Idle");
                walkingSounds.enabled = false;
            }

            yield return new WaitForSeconds(stepSoundDelay);
        }
    }
}
