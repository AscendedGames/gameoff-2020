using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeMusic : MonoBehaviour
{
    public Animator SceneMusic;
    public float FadeTime = 3f;

    private void Start()
    {
        SceneMusic.Play("fadeIn");
    }

    public void BtnFadeMusic()
    {
        StartCoroutine(FadeMenuMusic());
    }

    IEnumerator FadeMenuMusic ()
    {
        SceneMusic.Play("fadeOut");
        yield return new WaitForSeconds(FadeTime);
    }

}
