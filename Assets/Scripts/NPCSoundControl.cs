using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class NPCSoundControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource walkingSounds;
    [SerializeField]
    private float stepSoundDelay = 0.5f; // To define the stepDelay inside of Unity
    [SerializeField]
    private Animator NPCAnimation;

    private bool couroutineOn; // For Adding the Coroutine to delay the walking sounds
    private NPCPursuitController _npcPursuitController;

    // Start is called before the first frame update
    void Start()
    {
        _npcPursuitController = GetComponent<NPCPursuitController>();
        NPCAnimation = GetComponent<Animator>();
        couroutineOn = true;
        StartCoroutine(Patroling());
    }

    IEnumerator Patroling()
    {

        while (couroutineOn == true)
        {
            if (_npcPursuitController.IsInPursuit == false && _npcPursuitController.HasBrokenPursuit == false)
            {
                walkingSounds.enabled = true;
                NPCAnimation.Play("Scientist Walk");
                walkingSounds.pitch = (Random.Range(1f, 1.1f));
                walkingSounds.Play();
            }
            else if (_npcPursuitController.IsInPursuit == true)
            {
                walkingSounds.enabled = true;
                NPCAnimation.Play("Scientist Walk");
                walkingSounds.pitch = (Random.Range(1.3f, 1.5f));
                walkingSounds.Play();
            }
            else if (_npcPursuitController.HasBrokenPursuit == true)
            {
                NPCAnimation.Play("Scientist-Idle");
                walkingSounds.enabled = false;
            }

            yield return new WaitForSeconds(stepSoundDelay);
        }
    }
}
