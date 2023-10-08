using UnityEngine;

public class OrbitMoon : MonoBehaviour
{
    public Transform earth; // Earth transform as the central object
    public float orbitDuration = 27.3f; // Time for the Moon to complete one orbit around Earth in days
    public TimeController timeController;

    // Approximate values for the Moon's elliptical orbit around Earth
    public float semiMajorAxis = 384400f / 149597870.7f; // Scaled down to Unity units using Earth-to-Sun distance as reference
    public float semiMinorAxis = 383000f / 149597870.7f; // Scaled down likewise
    public float eccentricity = 0.0549f;
    public float orbitTilt = 5.145f; // Inclination of the Moon's orbit in degrees

    private float orbitSpeed;
    private Vector3 ellipseFocus;

    void Start()
    {
        orbitSpeed = 2 * Mathf.PI / orbitDuration; // Radians per day
        ellipseFocus = new Vector3(eccentricity * semiMajorAxis, 0, 0);
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;
        //float t = orbitSpeed * elapsedTime;
        float t = orbitSpeed * timeController.GlobalCurrentTime;

        float x = semiMajorAxis * Mathf.Cos(t);
        float z = semiMinorAxis * Mathf.Sin(t);
        x -= ellipseFocus.x;

        // Adding the orbital tilt
        float radTilt = Mathf.Deg2Rad * orbitTilt;
        float newY = Mathf.Sin(radTilt) * z;
        float newZ = Mathf.Cos(radTilt) * z;

        transform.position = earth.position + new Vector3(x, newY, newZ);
    }
}
