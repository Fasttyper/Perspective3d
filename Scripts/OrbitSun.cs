using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSun : MonoBehaviour
{
    public Transform sun;
    public float orbitDuration = 365.25f; // Time to complete one orbit in days
    public TimeController timeController;

    public float semiMajorAxis = 1f;        // Represents 1 AU
    public float semiMinorAxis = 0.9997f;   // Almost the same due to Earth's low eccentricity
    public float eccentricity = 0.0167f;    // Earth's eccentricity

    private float orbitSpeed;
    private Vector3 ellipseFocus;   // Position of the Sun as one of the ellipse's foci

    void Start()
    {
        orbitSpeed = 2 * Mathf.PI / orbitDuration; // Radians per day
        ellipseFocus = new Vector3(eccentricity * semiMajorAxis, 0, 0);
        sun.position += ellipseFocus;  // Move the sun to the focus
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;
        //float t = orbitSpeed * elapsedTime;
        float t = orbitSpeed * timeController.GlobalCurrentTime;

        float x = semiMajorAxis * Mathf.Cos(t);
        float z = semiMinorAxis * Mathf.Sin(t);

        // Adjust the position for the ellipse's focus
        transform.position = sun.position + new Vector3(x - ellipseFocus.x, 0, z);
    }
}
