using UnityEngine;
using UnityEngine.UI;

public class HidingController : MonoBehaviour
{
    public Text HiddenText;
    public Text DetectedText;

    [HideInInspector]
    public bool isMouseHidden = false;

    // Start is called before the first frame update
    void Start()
    {
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
            isMouseHidden = true;
            HiddenText.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("HidingArea"))
        {
            isMouseHidden = false;
            HiddenText.color = Color.red;
        }
    }
}
