using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Animator animator;

    private Queue<string> sentences;
    private string currentSceneName;

    void Start()
    {
        sentences = new Queue<string>();

        var activeScene = SceneManager.GetActiveScene();
        currentSceneName = activeScene.name;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        if (currentSceneName == "Opening")
        {
            FindObjectOfType<FadeMusic>().BtnFadeMusic();
            FindObjectOfType<LevelLoader>().TransitionToLevel("Level 1");
        }

        if (currentSceneName == "Ending")
        {
            FindObjectOfType<FadeMusic>().BtnFadeMusic();
            FindObjectOfType<LevelLoader>().TransitionToLevel("Thank You");
        }
    }
}
