using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class CameraOrbit : MonoBehaviour
{
    public Transform[] targets;
    public int currentTarget;
    public float distance = 10.0f; // The starting distance from the Sun.
    public float speed = 3.0f; // The speed of the rotation.
    public float zoomSpeed = 1.0f; // Speed of zooming in and out.
    public float minDistance = 5.0f; // Minimum distance from the Sun.
    public float maxDistance = 20.0f; // Maximum distance from the Sun.

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private void Update()
    {
        if (Input.GetMouseButton(0)) // If Left Button is pressed, the camera will rotate.
        {
            currentX += Input.GetAxis("Mouse X") * speed;
            currentY -= Input.GetAxis("Mouse Y") * speed;
            currentY = Mathf.Clamp(currentY, -90, 90); // Prevent camera flip
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            IncrementTarget();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);
        }
    }

    private void IncrementTarget()
    {
        if (targets.Length == 0) return;

        currentTarget = (currentTarget + 1) % targets.Length;

        //Debug.Log("Current Target: " + targets[currentTarget].name);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = targets[currentTarget].position + rotation * dir;
        transform.LookAt(targets[currentTarget].position);
    }
}
