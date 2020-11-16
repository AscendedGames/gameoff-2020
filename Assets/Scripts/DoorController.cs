using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float HoldOpenForSeconds;

    private GameObject door;
    private bool isDoorOpen = false;
    private bool isDoorClosing = false;
    private float holdOpenForSeconds;
    private Animator doorAnimation;
    public AudioSource doorOpen;
    public AudioSource doorClosed;

    void Start()
    {
        door = transform.parent.gameObject;
        doorAnimation = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorClosing && isDoorOpen)
        {
            if (holdOpenForSeconds > 0)
            {
                holdOpenForSeconds -= Time.deltaTime;
            }
            else CloseDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Scientist"))
        {
            isDoorClosing = false;

            if (!isDoorOpen)
            {
                holdOpenForSeconds = HoldOpenForSeconds;
                doorAnimation.Play("DoorOpenAnimation");
                doorOpen.Play();
                isDoorOpen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Scientist"))
        {
            if (isDoorOpen)
            {
                holdOpenForSeconds = HoldOpenForSeconds;
                isDoorClosing = true;
            }
        }
    }

    private void CloseDoor()
    {
        isDoorOpen = false;
        doorAnimation.Play("DoorCloseAnimation");
        doorClosed.Play();
        holdOpenForSeconds = HoldOpenForSeconds;
    }
}
