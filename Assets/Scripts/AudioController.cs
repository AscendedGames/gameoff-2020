using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    private void Update ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(1.7f, 3.0f);
        audioSource.volume = Random.Range(0.358f, 0.420f);
    }
}
