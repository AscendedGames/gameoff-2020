using System.Collections;
using TMPro;
using UnityEngine;

public class OutroController : MonoBehaviour
{
    public float dialogueDelayTime = 2.0f;

    public GameObject dialogueParent;
    public GameObject dialogueBox;

    public TMP_Text CurrentTimeText;
    public TMP_Text FastestTimeText;
    public TMP_Text TimeDifferenceText;

    private DialogueTrigger dialogueTrigger;

    void Start()
    {
        dialogueTrigger = dialogueParent.GetComponent<DialogueTrigger>();

        dialogueBox.SetActive(false);

        StartOutroDialogue();
        SetTimerDisplays();
    }

    public void StartOutroDialogue()
    {
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(dialogueDelayTime);

        dialogueBox.SetActive(true);
        dialogueTrigger.TriggerDialogue();
    }

    void SetTimerDisplays()
    {
        if (PlayerPrefs.HasKey("Latest Time")) CurrentTimeText.text = FormatTime(PlayerPrefs.GetFloat("Latest Time"));

        if (PlayerPrefs.HasKey("Fastest Time")) FastestTimeText.text = FormatTime(PlayerPrefs.GetFloat("Fastest Time"));

        if (PlayerPrefs.HasKey("Latest Time") && PlayerPrefs.HasKey("Fastest Time"))
        {
            float currentTime = PlayerPrefs.GetFloat("Latest Time");
            float fastestTime = PlayerPrefs.GetFloat("Fastest Time");

            TimeDifferenceText.text = FormatTime(currentTime - fastestTime);
        }
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }
}
