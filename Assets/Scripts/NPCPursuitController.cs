using UnityEngine;
using UnityEngine.UI;

public class NPCPursuitController : MonoBehaviour
{
    public Transform Player;
    public float PursuitSpeed;
    public GameObject VisionDetector;
    public Text GameOverText;

    [HideInInspector]
    public bool isNPCInPursuit;

    private bool isPlayerInSight;

    // Start is called before the first frame update
    void Start()
    {
        GameOverText.enabled = false;
        isPlayerInSight = false;
        isNPCInPursuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInSight = VisionDetector.GetComponent<VisionController>().isPlayerDetected;

        if (isPlayerInSight)
        {
            PursuePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameOverText.enabled = true;
        }
    }

    void PursuePlayer()
    {
        transform.LookAt(Player);
        transform.position += transform.forward * PursuitSpeed * Time.deltaTime;
    }
}
