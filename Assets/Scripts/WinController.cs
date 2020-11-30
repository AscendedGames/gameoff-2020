using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public Animator animator;

    public ScriptableObject musicFadeObject;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    public void PerformVictory()
    {
        GetComponent<FadeMusic>().BtnFadeMusic();

        animator.Play("Box Close");

        FindObjectOfType<LevelLoader>().TransitionToLevel("Ending");
    }
}
