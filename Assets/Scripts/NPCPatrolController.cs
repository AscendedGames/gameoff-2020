using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public Transform Player;
    public float MovementSpeed;
    public SpriteRenderer spriteRenderer;
    public Collider2D LeftPatrolBoundary;
    public Collider2D RightPatrolBoundary;

    [Header("Randomized Patrolling")]
    public bool EnableRandomPatrol;
    public float DirChangeTimeMin;
    public float DirChangeTimeMax;
    public float DirChangePauseTimeMin;
    public float DirChangePauseTimeMax;

    private BoxCollider2D npcBodyCollider;
    private bool hasGonePastCheckpoint;
    private float randomDirChangeTimer;
    private bool randomDirChangeTimerNeedsSet;

    private float _brokenPursuitPauseTimer;
    private bool _isInPursuit;
    private bool _hasBrokenPursuit;
    private float dirChangePauseTimer;

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
        randomDirChangeTimerNeedsSet = true;
        dirChangePauseTimer = SetDirChangePauseTimer();
    }

    void Update()
    {
        _npcPursuitController = GetComponent<NPCPursuitController>();
        _isInPursuit = _npcPursuitController.IsInPursuit;
        _hasBrokenPursuit = _npcPursuitController.HasBrokenPursuit;

        if (!_hasBrokenPursuit)
        {
            if (EnableRandomPatrol)
            {
                randomDirChangeTimer -= Time.deltaTime;

                if (DoRandomDirectionChange() && !hasGonePastCheckpoint)
                {
                    if (dirChangePauseTimer > 0)
                    {
                        PauseMovement();
                        dirChangePauseTimer -= Time.deltaTime;
                    }
                    else
                    {
                        randomDirChangeTimerNeedsSet = true;
                        dirChangePauseTimer = SetDirChangePauseTimer();
                        RotateNPCAndChildren();
                    }
                }
                else MoveNormally();
            }
            else MoveNormally();
        }
        else if (_hasBrokenPursuit)
        {
            _brokenPursuitPauseTimer -= Time.deltaTime;

            if (_brokenPursuitPauseTimer > 0)
            {
                if (!visionController.IsPlayerDetected)
                {
                    PauseMovement();
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

                RotateNPCAndChildren();
                MoveNormally();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && !_isInPursuit)
        {
            if (hasGonePastCheckpoint) hasGonePastCheckpoint = false;
            else RotateNPCAndChildren();
        }

        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && _isInPursuit)
        {
            hasGonePastCheckpoint = true;
        }
    }

    private void MoveNormally()
    {
        transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
    }
    
    private bool DoRandomDirectionChange()
    {
        if (randomDirChangeTimerNeedsSet) ResetDirChangeTimer();

        if (randomDirChangeTimer > 0) return false;
        else return true;
    }

    private void RotateNPCAndChildren()
    {
        transform.localRotation *= Quaternion.Euler(0, 180, 0);
        transform.Find("OverheadStatus").localRotation *= Quaternion.Euler(0, 180, 0);
    }

    private void PauseMovement()
    {
        transform.Translate(0, 0, 0);
    }

    private float SetDirChangePauseTimer()
    {
        return Random.Range(DirChangePauseTimeMin, DirChangePauseTimeMax);
    }

    private void ResetDirChangeTimer()
    {
        randomDirChangeTimer = Random.Range(DirChangeTimeMin, DirChangeTimeMax);
        randomDirChangeTimerNeedsSet = false;
    }
}