using UnityEngine;
using UnityEngine.UI;

public class NPCPursuitController : MonoBehaviour
{
    public Transform Player;
    public float PursuitSpeed;
    public GameObject VisionDetector;
    public Text GameOverText;
    public float BrokenPursuitPauseTime;

    [HideInInspector]
    public bool IsInPursuit;
    [HideInInspector]
    public bool HasBrokenPursuit;

    private VisionController _visionController;
    private bool _isPlayerInSight;
    private bool _isPlayerInRange;
    private GameObject _overheadStatus;

    // Start is called before the first frame update
    void Start()
    {
        _visionController = VisionDetector.GetComponent<VisionController>();

        GameOverText.enabled = false;
        _isPlayerInSight = false;
        IsInPursuit = false;
        HasBrokenPursuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        _isPlayerInSight = _visionController.IsPlayerDetected;
        _isPlayerInRange = _visionController.IsPlayerInRange;

        if (_isPlayerInSight)
        {
            IsInPursuit = true;
            PursuePlayer();
        }
        else
        {
            if (IsInPursuit) HasBrokenPursuit = true;
            IsInPursuit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_isPlayerInRange)
            {
                transform.localRotation *= Quaternion.Euler(0, 90, 0);
            }
            GameOverText.enabled = true;
            Time.timeScale = 0;
        }
    }

    void PursuePlayer()
    {
        transform.Translate(2 * Time.deltaTime * PursuitSpeed, 0, 0);
    }

    public void ResetHasBrokenPursuit()
    {
        HasBrokenPursuit = false;
    }
}
