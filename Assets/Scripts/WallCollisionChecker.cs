using UnityEngine;

public class WallCollisionChecker : MonoBehaviour
{
    public float WallRaycastDistance = 1.0f;
    public float originOffset = 0.1f;

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();
    }

    public RaycastHit2D[] PerformRaycast(Vector2 direction)
    {
        float directionOriginOffset = originOffset * (direction.x > 0 ? 1 : -1);

        Vector2 startingPosition = new Vector2(transform.position.x + directionOriginOffset, transform.position.y);

        return Physics2D.RaycastAll(startingPosition, direction, WallRaycastDistance);
    }

    public void CheckRaycast()
    {
        Vector2 direction = new Vector2(1, 0);

        if (transform.forward.x < 0) direction *= -1;

        RaycastHit2D[] hits = PerformRaycast(direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Enemy" || hit.transform.tag == "Trigger") continue;

            if (hit.transform.tag == "Level")
            {
                transform.parent.localRotation *= Quaternion.Euler(0, 180, 0);
                transform.parent.Find("OverheadStatus").localRotation *= Quaternion.Euler(0, 180, 0);
            }
        }
    }
}