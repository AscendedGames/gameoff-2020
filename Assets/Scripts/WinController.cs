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

        FindObjectOfType<LevelLoader>().TransitionToLevel("Ending");
    }
}
