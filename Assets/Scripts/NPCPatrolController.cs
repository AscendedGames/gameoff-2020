using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public float MovementSpeed;
    public SpriteRenderer spriteRenderer;
    public Collider2D LeftPatrolBoundary;
    public Collider2D RightPatrolBoundary;

    private BoxCollider2D npcBodyCollider;

    private void Start()
    {
        npcBodyCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.Equals(LeftPatrolBoundary.name) || collision.name.Equals(RightPatrolBoundary.name)) && npcBodyCollider.IsTouching(collision) && !GetComponent<NPCPursuitController>().isNPCInPursuit)
        {
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
