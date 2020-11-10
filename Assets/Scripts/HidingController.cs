using UnityEngine;
using UnityEngine.UI;

public class HidingController : MonoBehaviour
{
    public Text HiddenText;
    public Text DetectedText;

    [HideInInspector]
    public bool isMouseHidden;

    // Start is called before the first frame update
    void Start()
    {
        isMouseHidden = false;
        DetectedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("HidingArea"))
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Hidden");
            transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Hidden");
            isMouseHidden = true;
            HiddenText.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("HidingArea"))
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            transform.Find("WallCollider").gameObject.layer = LayerMask.NameToLayer("Default");
            isMouseHidden = false;
            HiddenText.color = Color.red;
        }
    }
}
