using UnityEngine;

public class NPCPatrolController : MonoBehaviour
{
    public float MovementSpeed;
    public bool MoveRightFirst = true;

    void Update()
    {
        if (MoveRightFirst)
        {
            transform.Translate(2 * Time.deltaTime * MovementSpeed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * MovementSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Prototype Scientist Boundary"))
        {
            if (MoveRightFirst) MoveRightFirst = false;
            else MoveRightFirst = true;
        }
    }
}
