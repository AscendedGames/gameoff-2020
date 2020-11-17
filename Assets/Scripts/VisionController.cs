using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public GameObject Player;
    public GameObject NPCEyes;

    [Header("Overhead Status")]
    public GameObject OverheadStatus;
    public Sprite AlertedSprite;
    public Sprite SearchingSprite;

    [HideInInspector]
    public bool IsPlayerDetected;
    [HideInInspector]
    public bool IsPlayerInRange;

    private SpriteRenderer overheadStatus;

    // Start is called before the first frame update
    void Start()
    {
        overheadStatus = OverheadStatus.GetComponent<SpriteRenderer>();
        IsPlayerInRange = false;
        IsPlayerDetected = false;
        overheadStatus.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanPlayerBeSeen() && !Player.GetComponent<HidingController>().isPlayerHidden)
        {
            Player.GetComponent<HidingController>().DetectedText.enabled = true;
            IsPlayerDetected = true;
            overheadStatus.sprite = AlertedSprite;
        }
        else
        {
            Player.GetComponent<HidingController>().DetectedText.enabled = false;
            IsPlayerDetected = false;
            overheadStatus.sprite = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player))
        {
            IsPlayerInRange = false;
            IsPlayerDetected = false;
        }
    }

    bool CanPlayerBeSeen()
    {
        if (IsPlayerInRange && !PlayerHiddenByObstacles()) return true;
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