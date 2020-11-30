using UnityEngine;
using UnityEngine.UI;

public class HidingController : MonoBehaviour
{
    public Text HiddenText;
    public Text DetectedText;
    public GameObject Player;
    public AudioSource HidingSound;

    [HideInInspector]
    public bool isPlayerHidden;

    private bool isInHiddenArea;
    private Vector3 hidingSpotCoords;
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;
    private bool hasPerformedVictory;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _rigidBody2D = transform.GetComponent<Rigidbody2D>();

        isPlayerHidden = false;
        isInHiddenArea = false;
        hidingSpotCoords = new Vector3(0, 0, 0);
        DetectedText.enabled = false;
        hasPerformedVictory = false;
        // Get the Player GameObject

    }

    // Update is called once per frame
    void Update()
    {
        CheckHideButtonsPressed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("HidingArea") || collision.gameObject.name.Equals("TutorialHidingArea") || collision.gameObject.name.Equals("Win Hiding Area"))
        {
            hidingSpotCoords = collision.gameObject.transform.position;
            isInHiddenArea = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Win Hiding Area") && isPlayerHidden && !hasPerformedVictory)
        {
            hasPerformedVictory = true;
            FindObjectOfType<WinController>().PerformVictory();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("HidingArea") || collision.gameObject.name.Equals("TutorialHidingArea") || collision.gameObject.name.Equals("Win Hiding Area"))
        {
            isInHiddenArea = false;
        }
    }

    private void CheckHideButtonsPressed()
    {
        if (Input.GetAxis("Vertical") < 0 && isInHiddenArea && isPlayerHidden == false)
        {
            HidingSound.Play();
            HidePlayer();
            // Pauses Animatorc of Player
            Player.GetComponent<Animator>().Play("Mouse-hide");
            DisablePlayerMovement();
        }

        if (Input.GetAxis("Vertical") > 0 && isPlayerHidden)
        {
            UnhidePlayer();
            Player.GetComponent<Animator>().Play("Mouse-Idle-4Legs");
            EnablePlayerMovement();
        }
    }

    private void HidePlayer()
    {
        _spriteRenderer.sortingOrder = 0;
        _spriteRenderer.sortingLayerName = "Foreground Sprites";
        transform.gameObject.layer = LayerMask.NameToLayer("Hidden");
        transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Hidden");
        MovePlayerToHidingSpot();
        isPlayerHidden = true;
        HiddenText.color = Color.green;
    }

    private void UnhidePlayer()
    {
        _spriteRenderer.sortingOrder = 3;
        _spriteRenderer.sortingLayerName = "Player Character";
        transform.gameObject.layer = LayerMask.NameToLayer("Default");
        transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Default");
        isPlayerHidden = false;
        HiddenText.color = Color.red;
    }

    private void DisablePlayerMovement()
    {
        _playerController.PlayerCanMove = false;
    }

    private void EnablePlayerMovement()
    {
        _playerController.PlayerCanMove = true;
    }

    private void MovePlayerToHidingSpot()
    {
        _rigidBody2D.velocity = Vector3.zero;
        transform.position = new Vector3(hidingSpotCoords.x, transform.position.y, transform.position.z);
    }
}
