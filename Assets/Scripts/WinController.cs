using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public Animator animator;
    public ScriptableObject musicFadeObject;

    private TimerController timerController;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        timerController = FindObjectOfType<TimerController>();
    }

    public void PerformVictory()
    {
        GetComponent<FadeMusic>().BtnFadeMusic();

        animator.Play("Box Close");

        timerController.doRunTimer = false;

        float currentTime = timerController.time;

        PlayerPrefs.SetFloat("Lastest Time", currentTime);

        float fastestTime = PlayerPrefs.HasKey("Fastest Time") ? PlayerPrefs.GetFloat("Fastest Time") : 0.0f;

        if (currentTime < fastestTime) PlayerPrefs.SetFloat("Fastest Time", timerController.time);

        FindObjectOfType<LevelLoader>().TransitionToLevel("Ending");
    }
}
