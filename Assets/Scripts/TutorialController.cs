using System;
using UnityEngine;

#pragma warning disable 0649

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpWaypoint;
    [SerializeField]
    private GameObject hideWaypoint;
    [SerializeField]
    private GameObject hidingSpot;
    [SerializeField]
    private GameObject goodluckWaypoint;

    [SerializeField]
    private GameObject jumpMessage;
    [SerializeField]
    private GameObject hideMessage;
    [SerializeField]
    private GameObject unhideMessage;
    [SerializeField]
    private GameObject goodluckMessage;

    [SerializeField]
    private float messageActiveTime = 6.0f;

    private HidingController _hidingController;

    private float _messageActiveTime;
    private bool startTimer;
    private bool hasPlayerHidden;
    private bool isTutorialCompleted;

    private bool jumpStepPassed;
    private bool hideStepPassed;
    private bool unhideStepPassed;
    private bool goodluckStepPassed;

    // Start is called before the first frame update
    void Start()
    {
        jumpMessage.SetActive(false);
        hideMessage.SetActive(false);
        unhideMessage.SetActive(false);
        goodluckMessage.SetActive(false);

        jumpStepPassed = false;
        hideStepPassed = false;
        unhideStepPassed = false;
        goodluckStepPassed = false;

        _messageActiveTime = messageActiveTime;
        hasPlayerHidden = false;
        isTutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt("TutorialCompleted", 0));

        _hidingController = transform.GetComponent<HidingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer) _messageActiveTime -= Time.deltaTime;

        if (_messageActiveTime < 0) HideGoodluckMessage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(jumpWaypoint.name) && !isTutorialCompleted && !jumpStepPassed)
        {
            jumpMessage.SetActive(true);
        }

        if (collision.name.Equals(hideWaypoint.name) && !isTutorialCompleted && !hideStepPassed)
        {
            jumpStepPassed = true;

            jumpMessage.SetActive(false);
            hideMessage.SetActive(true);
        }

        if (collision.name.Equals(goodluckWaypoint.name) && hasPlayerHidden && !isTutorialCompleted && !goodluckStepPassed)
        {
            startTimer = true;
            unhideStepPassed = true;

            goodluckMessage.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals(hidingSpot.name) && IsPlayerHidden() && !isTutorialCompleted && !unhideStepPassed)
        {
            hideStepPassed = true;
            hideMessage.SetActive(false);
            unhideMessage.SetActive(true);

            hasPlayerHidden = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(hidingSpot.name) && hasPlayerHidden && !isTutorialCompleted)
        {
            unhideMessage.SetActive(false);
        }
    }

    bool IsPlayerHidden()
    {
        return _hidingController.isPlayerHidden;
    }

    void HideGoodluckMessage()
    {
        goodluckStepPassed = true;

        goodluckMessage.SetActive(false);

        _messageActiveTime = messageActiveTime;

        PlayerPrefs.SetInt("TutorialCompleted", 1);
    }
}