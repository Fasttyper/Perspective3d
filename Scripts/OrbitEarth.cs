using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEarth : MonoBehaviour
{
    public Transform earth; // Assign the Earth transform here in Inspector.
    public float orbitDuration = 27.32f; // Time to complete one orbit in days

    private float orbitSpeed;

    void Start()
    {
        orbitSpeed = 360f / orbitDuration; // Degrees per day
    }

    void Update()
    {
        float angle = orbitSpeed * Time.deltaTime; // Degrees per frame
        transform.RotateAround(earth.position, Vector3.up, angle);
    }
}
