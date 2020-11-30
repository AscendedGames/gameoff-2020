using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2.0f;

    void Update()
    {
        
    }

    public void TransitionToLevel(string levelName)
    {
        Debug.Log("TransitionToLevel");
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log("We're here");
        SceneManager.LoadScene(levelName);
    }
}
