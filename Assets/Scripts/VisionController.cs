using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public GameObject PlayerMouse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(PlayerMouse) && PlayerMouse.GetComponent<HidingController>().isMouseHidden == false)
        {
            Debug.Log("DETECTED!");
        }
    }
}
