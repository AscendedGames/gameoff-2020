using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public GameObject Player;
    public GameObject NPCEyes;

    [HideInInspector]
    public bool isPlayerDetected;

    private bool isPlayerInRange;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        isPlayerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanPlayerBeSeen() && !Player.GetComponent<HidingController>().isMouseHidden)
        {
            Player.GetComponent<HidingController>().DetectedText.enabled = true;
            isPlayerDetected = true;
        }
        else
        {
            Player.GetComponent<HidingController>().DetectedText.enabled = false;
            isPlayerDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player))
        {
            isPlayerInRange = false;
            isPlayerDetected = false;
        }
    }

    bool CanPlayerBeSeen()
    {
        if (isPlayerInRange && !PlayerHiddenByObstacles()) return true;
        else return false;
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