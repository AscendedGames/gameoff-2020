using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float HoldOpenForSeconds;

    GameObject readonly door;
    bool initiateDoorActuation = false;
    bool isDoorOpen = false;
    float holdOpenForSeconds;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Scientist"))
        {
            if (!isDoorOpen)
            {
                distanceToActuate = DistanceToActuate;
                openUnitsPerFrame = OpenUnitsPerFrame;
                initiateDoorActuation = true;

                Debug.Log($"distanceToActuate set to {distanceToActuate}");
                Debug.Log($"openUnitsPerFrame set to {openUnitsPerFrame}");
                Debug.Log($"isDoorOpen set to {isDoorOpen}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Scientist"))
        {
            if (isDoorOpen)
            {
                doorOpenForSeconds = HoldOpenForSeconds;
                distanceToActuate = DistanceToActuate;
                closeUnitsPerFrame = CloseUnitsPerFrame;
                isDoorOpen = false;

                Debug.Log($"doorOpenForSeconds set to {doorOpenForSeconds}");
                Debug.Log($"distanceToActuate set to {distanceToActuate}");
                Debug.Log($"closeUnitsPerFrame set to {closeUnitsPerFrame}");
                Debug.Log($"isDoorOpen set to {isDoorOpen}");
            }
        }
    }

    void PerformOpenDoor()
    {
        Debug.Log("PerformDoorOpen() called");

        distanceToActuate -= openUnitsPerFrame;

        door.transform.Translate(0, OpenUnitsPerFrame * Time.deltaTime * OpenSpeed, 0);
    }

    void PerformCloseDoor()
    {
        Debug.Log("PerformDoorClose() called");

        door.transform.Translate(0, CloseUnitsPerFrame * Time.deltaTime * OpenSpeed, 0);
    }
}
