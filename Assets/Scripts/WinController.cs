using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    public void PerformVictory()
    {
        animator.Play("Box Close");

        FindObjectOfType<LevelLoader>().TransitionToLevel("Ending");
    }
}
