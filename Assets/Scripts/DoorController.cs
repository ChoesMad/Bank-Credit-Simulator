using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openDistance = 2f;
    public float speed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool playerInside = false;

    private BoxCollider leftCol;
    private BoxCollider rightCol;

    void Start()
    {
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;

        leftOpenPos = leftClosedPos + Vector3.left * openDistance;
        rightOpenPos = rightClosedPos + Vector3.right * openDistance;

        leftCol = leftDoor.GetComponentInChildren<BoxCollider>();
        rightCol = rightDoor.GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        if (leftDoor == null || rightDoor == null) return;

        if (playerInside)
            OpenDoors();
        else
            CloseDoors();
    }

    void OpenDoors()
    {
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpenPos, speed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpenPos, speed * Time.deltaTime);

        if (leftCol != null) leftCol.enabled = false;
        if (rightCol != null) rightCol.enabled = false;
    }

    void CloseDoors()
    {
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftClosedPos, speed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightClosedPos, speed * Time.deltaTime);

        if (Vector3.Distance(leftDoor.localPosition, leftClosedPos) < 0.01f)
        {
            if (leftCol != null) leftCol.enabled = true;
            if (rightCol != null) rightCol.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}