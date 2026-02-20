using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float distance = 2f;
    public float height = 1.5f;
    public float minDistance = 1f;
    public float mouseSensitivity = 400f;
    float xRotation = 20f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 70f);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

        Vector3 targetPosition = target.position + Vector3.up * height;
        Vector3 desiredCameraPos = targetPosition + rotation * new Vector3(0, 0, -distance);

        RaycastHit hit;

        if (Physics.Linecast(targetPosition, desiredCameraPos, out hit))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = desiredCameraPos;
        }

        transform.LookAt(targetPosition);
    }
}