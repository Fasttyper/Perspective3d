using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawOrbit : MonoBehaviour
{
    public Transform centerObject; // The object around which this orbit is drawn, e.g., Sun for Earth.
    public float semiMajorAxis = 1f; // Semi-major axis of the ellipse.
    public float semiMinorAxis = 0.9997f; // Semi-minor axis of the ellipse.
    public float eccentricity = 0.0167f; // Eccentricity of the ellipse.
    public int segments = 360; // The number of segments to use for the full circle.
    public float orbitTilt = 5f; // The tilt of the orbit in degrees.

    private Vector3 ellipseFocus; // Position of the centerObject as one of the ellipse's foci.
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true; // Using world space to allow orbit following the center object
        line.positionCount = segments + 1;

        ellipseFocus = new Vector3(eccentricity * semiMajorAxis, 0, 0);
    }

    void Update()
    {
        DrawEllipse();
    }

    void DrawEllipse()
    {
        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * semiMajorAxis;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * semiMinorAxis;

            // Adjust the position for the ellipse's focus
            x -= ellipseFocus.x;

            // Calculating the tilt of the orbit
            float radTilt = Mathf.Deg2Rad * orbitTilt;
            float newY = Mathf.Sin(radTilt) * z; // y-coordinate after tilt.
            float newZ = Mathf.Cos(radTilt) * z; // z-coordinate after tilt.

            Vector3 pos = new Vector3(x, newY, newZ) + centerObject.position;
            line.SetPosition(i, pos);
            angle += 360f / segments;
        }
    }
}
