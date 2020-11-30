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
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        if (currentSceneName == "Opening") FindObjectOfType<LevelLoader>().TransitionToLevel("Level 1");
    }
}
