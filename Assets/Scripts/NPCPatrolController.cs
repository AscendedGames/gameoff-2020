using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public float MovementSpeed;
    public SpriteRenderer spriteRenderer;

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
        if (collision.gameObject.name.Contains("Prototype Scientist Boundary") && npcBodyCollider.IsTouching(collision))
        {
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
