using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public GameObject PlayerMouse;
    public GameObject NPCEyes;

    private bool isPlayerInRange;
    private bool isVisionBlocked;

    [SerializeField]
    private Transform lineOfSightEnd;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanPlayerBeSeen())
        {
            Debug.Log("DETECTED!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(PlayerMouse) && PlayerMouse.GetComponent<HidingController>().isMouseHidden == false)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(PlayerMouse))
        {
            isPlayerInRange = false;
        }
    }

    bool PlayerInFieldOfView()
    {
        Vector2 directionToPlayer = PlayerMouse.transform.position - NPCEyes.transform.position;
        Debug.DrawLine(NPCEyes.transform.position, PlayerMouse.transform.position, Color.white);

        Vector2 lineOfSight = lineOfSightEnd.position - NPCEyes.transform.position;
        Debug.DrawLine(NPCEyes.transform.position, lineOfSightEnd.position, Color.cyan);

        float angle = Vector2.Angle(directionToPlayer, lineOfSight);

        if (angle < 170)
            return true;
        else
            return false;
    }

    bool CanPlayerBeSeen()
    {
        if (isPlayerInRange)
        {
            if (PlayerInFieldOfView())
                return (!PlayerHiddenByObstacles());
            else
                return false;

        }
        else
        {
            return false;
        }
    }

    bool PlayerHiddenByObstacles()
    {

        float distanceToPlayer = Vector2.Distance(NPCEyes.transform.position, PlayerMouse.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(NPCEyes.transform.position, PlayerMouse.transform.position - NPCEyes.transform.position, distanceToPlayer);
        Debug.DrawRay(NPCEyes.transform.position, PlayerMouse.transform.position - transform.position, Color.blue);
        List<float> distances = new List<float>();

        foreach (RaycastHit2D hit in hits)
        {
            // ignore the enemy's own colliders (and other enemies)
            if (hit.transform.tag == "Enemy")
                continue;

            // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
            if (hit.transform.tag != "Player")
            {
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        return false;

    }
}
