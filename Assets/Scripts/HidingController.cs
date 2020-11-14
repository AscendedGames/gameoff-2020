using UnityEngine;
using UnityEngine.UI;

public class HidingController : MonoBehaviour
{
    public Text HiddenText;
    public Text DetectedText;

    [HideInInspector]
    public bool isMouseHidden;

    private bool isInHiddenArea;
    private Vector3 hidingSpotCoords;
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _rigidBody2D = transform.GetComponent<Rigidbody2D>();

        isMouseHidden = false;
        isInHiddenArea = false;
        hidingSpotCoords = new Vector3(0, 0, 0);
        DetectedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHideButtonsPressed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("HidingArea"))
        {
            hidingSpotCoords = collision.gameObject.transform.position;
            isInHiddenArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("HidingArea"))
        {
            isInHiddenArea = false;
        }
    }

    private void CheckHideButtonsPressed()
    {
        if (Input.GetAxis("Vertical") < 0 && isInHiddenArea)
        {
            HidePlayer();
            DisablePlayerMovement();
        }

        if (Input.GetAxis("Vertical") > 0 && isMouseHidden)
        {
            UnhidePlayer();
            EnablePlayerMovement();
        }
    }

    private void HidePlayer()
    {
        _spriteRenderer.sortingOrder = 1;
        transform.gameObject.layer = LayerMask.NameToLayer("Hidden");
        transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Hidden");
        MovePlayerToHidingSpot();
        isMouseHidden = true;
        HiddenText.color = Color.green;
    }

    private void UnhidePlayer()
    {
        _spriteRenderer.sortingOrder = 3;
        transform.gameObject.layer = LayerMask.NameToLayer("Default");
        transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Default");
        isMouseHidden = false;
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
