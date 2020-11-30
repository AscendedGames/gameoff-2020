using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public float delayTime = 3.0f;
    private LevelLoader levelLoader;

    void Start()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        GoToMainMenu();
    }

    void GoToMainMenu()
    {
        StartCoroutine(TransitionToMainMenu());
    }

    IEnumerator TransitionToMainMenu()
    {

        yield return new WaitForSeconds(delayTime);

        levelLoader.TransitionToLevel("MainMenu");
    }
}
