using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EclipseDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Eclipse happening !!! ");
    }
}
