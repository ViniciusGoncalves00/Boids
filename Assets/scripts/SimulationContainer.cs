using System;
using UnityEngine;

public class SimulationContainer : MonoBehaviour
{
    public Vector3 Size;

    [Header("ONLY VIEW")]
    public Vector3 Min; 
    public Vector3 Max;

    private void Update()
    {
        Min = transform.position - Size / 2;
        Max = transform.position + Size / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Size);
        Min = transform.position - Size / 2;
        Max = transform.position + Size / 2;
    }
}
