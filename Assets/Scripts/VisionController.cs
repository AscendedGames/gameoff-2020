using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public GameObject Player;
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
        if (CanPlayerBeSeen() && !Player.GetComponent<HidingController>().isMouseHidden)
        {
            Player.GetComponent<HidingController>().DetectedText.enabled = true;
        }
        else Player.GetComponent<HidingController>().DetectedText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player) && Player.GetComponent<HidingController>().isMouseHidden == false)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player))
        {
            isPlayerInRange = false;
        }
    }

    bool PlayerInFieldOfView()
    {
        Vector2 directionToPlayer = Player.transform.position - NPCEyes.transform.position;
        Debug.DrawLine(NPCEyes.transform.position, Player.transform.position, Color.white);

        Vector2 lineOfSight = lineOfSightEnd.position - NPCEyes.transform.position;
        Debug.DrawLine(NPCEyes.transform.position, lineOfSightEnd.position, Color.cyan);

        float angle = Vector2.Angle(directionToPlayer, lineOfSight);

        if (angle < 170)
        {
            return true;
        }
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
        float distanceToPlayer = Vector2.Distance(NPCEyes.transform.position, Player.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(NPCEyes.transform.position, Player.transform.position - NPCEyes.transform.position, distanceToPlayer);
        Debug.DrawRay(NPCEyes.transform.position, Player.transform.position - NPCEyes.transform.position, Color.blue);
        List<float> distances = new List<float>();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Enemy" || hit.transform.tag == "Trigger")
                continue;

            if (hit.transform.tag != "Player")
            {
                return true;
            }
        }

        return false;
    }
}