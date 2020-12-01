using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public Animator animator;
    public ScriptableObject musicFadeObject;

    private TimerController timerController;
    private float fastestTime = 0.0f;

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

        PlayerPrefs.SetFloat("Latest Time", currentTime);

        if (PlayerPrefs.HasKey("Fastest Time"))
        {
            fastestTime = PlayerPrefs.GetFloat("Fastest Time");
        }
        else PlayerPrefs.SetFloat("Fastest Time", currentTime);

        if (currentTime < fastestTime) PlayerPrefs.SetFloat("Fastest Time", timerController.time);

        FindObjectOfType<LevelLoader>().TransitionToLevel("Ending");
    }
}
