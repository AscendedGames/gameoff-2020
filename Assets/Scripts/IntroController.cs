using System.Collections;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public float dialogueDelayTime = 2.0f;

    public GameObject dialogueParent;
    public GameObject dialogueBox;

    private DialogueTrigger dialogueTrigger;

    void Start()
    {
        dialogueTrigger = dialogueParent.GetComponent<DialogueTrigger>();

        dialogueBox.SetActive(false);

        StartIntroDialogue();
    }

    public void StartIntroDialogue()
    {
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(dialogueDelayTime);

        dialogueBox.SetActive(true);
        dialogueTrigger.TriggerDialogue();
    }
}
