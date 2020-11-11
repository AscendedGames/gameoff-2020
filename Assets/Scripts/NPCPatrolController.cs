using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public Transform Player;
    public float MovementSpeed;
    public SpriteRenderer spriteRenderer;
    public Collider2D LeftPatrolBoundary;
    public Collider2D RightPatrolBoundary;
    public float DirChangeTimerMin;
    public float DirChangeTimerMax;

    private BoxCollider2D npcBodyCollider;
    private bool hasGonePastCheckpoint;

    private float _brokenPursuitPauseTimer;
    private bool _isInPursuit;
    private bool _hasBrokenPursuit;

    private NPCPursuitController _npcPursuitController;
    private GameObject _overheadStatus;
    private GameObject _visionDetector;
    private SpriteRenderer _status;
    private VisionController visionController;

    void Start()
    {
        _overheadStatus = transform.Find("OverheadStatus").gameObject;
        _visionDetector = transform.Find("VisionDetector").gameObject;
        visionController = _visionDetector.GetComponent<VisionController>();
        npcBodyCollider = GetComponent<BoxCollider2D>();
        _npcPursuitController = GetComponent<NPCPursuitController>();
        _status = _overheadStatus.GetComponent<SpriteRenderer>();
        hasGonePastCheckpoint = false;
        _brokenPursuitPauseTimer = _npcPursuitController.BrokenPursuitPauseTime;
    }

    void Update()
    {
        _npcPursuitController = GetComponent<NPCPursuitController>();
        _isInPursuit = _npcPursuitController.IsInPursuit;
        _hasBrokenPursuit = _npcPursuitController.HasBrokenPursuit;

        if (!_hasBrokenPursuit)
        {
            transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
        }
        else if (_hasBrokenPursuit)
        {
            _brokenPursuitPauseTimer -= Time.deltaTime;

            if (_brokenPursuitPauseTimer > 0)
            {
                if (!visionController.IsPlayerDetected)
                {
                    transform.Translate(0, 0, 0);
                    _status.sprite = visionController.SearchingSprite;
                }
                else 
                {
                    _npcPursuitController.HasBrokenPursuit = false;
                    _brokenPursuitPauseTimer = _npcPursuitController.BrokenPursuitPauseTime;
                }
            }
            else if (_brokenPursuitPauseTimer < 0)
            {
                _npcPursuitController.HasBrokenPursuit = false;
                _brokenPursuitPauseTimer = _npcPursuitController.BrokenPursuitPauseTime;

                transform.localRotation *= Quaternion.Euler(0, 180, 0);
                transform.Find("OverheadStatus").localRotation *= Quaternion.Euler(0, 180, 0);
                transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && !_isInPursuit)
        {
            if (hasGonePastCheckpoint) hasGonePastCheckpoint = false;
            else
            {
                transform.localRotation *= Quaternion.Euler(0, 180, 0);
                transform.Find("OverheadStatus").localRotation *= Quaternion.Euler(0, 180, 0);
            }
        }

        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && _isInPursuit)
        {
            hasGonePastCheckpoint = true;
            Debug.Log(hasGonePastCheckpoint);
        }
    }
}