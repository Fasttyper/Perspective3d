using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtThis : MonoBehaviour
{
    public Transform Target;
    void Update()
    {
        transform.LookAt(Target);
    }
}
