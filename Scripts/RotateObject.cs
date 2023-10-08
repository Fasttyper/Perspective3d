using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationDuration = 1f;  // Number of full rotations per second.
    public TimeController timeController;

    void Update()
    {
        float totalDegreesRotated = 360f * rotationDuration * timeController.GlobalCurrentTime;
        transform.rotation = Quaternion.Euler(new Vector3(0f, totalDegreesRotated, 0f));
    }
}
