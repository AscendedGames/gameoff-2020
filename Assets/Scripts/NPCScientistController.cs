using UnityEngine;

public class NPCScientistController : MonoBehaviour
{
    public float MovementSpeed;
    public bool MoveRight = true;

    void Update()
    {
        if (MoveRight)
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
        if(collision.gameObject.name.Contains("Prototype Scientist Boundary"))
        {
            if (MoveRight) MoveRight = false;
            else MoveRight = true;
        }
    }
}
