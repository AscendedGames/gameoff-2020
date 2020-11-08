using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public float MovementSpeed;
    public SpriteRenderer spriteRenderer;
    public Collider2D LeftPatrolBoundary;
    public Collider2D RightPatrolBoundary;

    private BoxCollider2D npcBodyCollider;
    private bool hasGonePastCollider;

    private float _brokenPursuitPauseTimer;
    private bool _isInPursuit;
    private bool _hasBrokenPursuit;

    private NPCPursuitController npcPursuitController;
    private GameObject OverheadStatus;
    private GameObject VisionDetector;
    private SpriteRenderer overheadStatus;
    private VisionController visionController;

    private void Start()
    {
        OverheadStatus = transform.Find("OverheadStatus").gameObject;
        VisionDetector = transform.Find("VisionDetector").gameObject;
        visionController = VisionDetector.GetComponent<VisionController>();
        npcBodyCollider = GetComponent<BoxCollider2D>();
        npcPursuitController = GetComponent<NPCPursuitController>();
        overheadStatus = OverheadStatus.GetComponent<SpriteRenderer>();
        hasGonePastCollider = false;
        _brokenPursuitPauseTimer = npcPursuitController.BrokenPursuitPauseTime;
    }

    void Update()
    {
        npcPursuitController = GetComponent<NPCPursuitController>();
        _isInPursuit = npcPursuitController.IsInPursuit;
        _hasBrokenPursuit = npcPursuitController.HasBrokenPursuit;

        if (!_hasBrokenPursuit)
        {
            transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
        }
        else if (_hasBrokenPursuit)
        {
            _brokenPursuitPauseTimer -= Time.deltaTime;

            if (_brokenPursuitPauseTimer > 0)
            {
                transform.Translate(0, 0, 0);
                overheadStatus.sprite = visionController.SearchingSprite;
            }
            else
            {
                npcPursuitController.HasBrokenPursuit = false;
                _brokenPursuitPauseTimer = npcPursuitController.BrokenPursuitPauseTime;

                transform.localRotation *= Quaternion.Euler(0, 180, 0);
                transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && !_isInPursuit)
        {
            if (hasGonePastCollider) hasGonePastCollider = false;
            else transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }

        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && _isInPursuit)
        {
            hasGonePastCollider = true;
            Debug.Log(hasGonePastCollider);
        }
    }
}